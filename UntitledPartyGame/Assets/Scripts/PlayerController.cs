using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 10f;
    public float gravity = 9.81f;
    public GameObject speakerObject;
    
    public AudioClip muffledMusic;
    public AudioClip normalMusic;

    AudioSource speaker;
    AudioSource crowdSFX;
    CharacterController controller;
    Vector3 input;
    Vector3 moveDir;
    float currTime = 0;
    //bool inHouse = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speaker = speakerObject.GetComponent<AudioSource>();
        crowdSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        input = (transform.right * moveHoriz + transform.forward * moveVert).normalized;
        input *= playerSpeed;
        currTime = speaker.time;
        if (controller.isGrounded)
        {
            moveDir = input;
            moveDir.y = 0.0f;
        }
        else
        {
            input.y = moveDir.y;
            moveDir = Vector3.Lerp(moveDir, input, Time.deltaTime);
        }
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(input * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collide");
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ground");
            speaker.clip = muffledMusic;
            speaker.time = currTime;
            speaker.Play();
            crowdSFX.Stop();
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Floor");
            speaker.clip = normalMusic;
            speaker.time = currTime;
            speaker.Play();
            crowdSFX.Play();
        }
    }
}
