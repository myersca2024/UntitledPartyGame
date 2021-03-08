using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 10f;
    public float gravity = 9.81f;
    public GameObject speakerObject;
    public bool speakersDestroyed = false;
    public AudioClip muffledMusic;
    public AudioClip normalMusic;

    AudioSource speaker;
    AudioSource crowdSFX;
    CharacterController controller;
    Vector3 input;
    Vector3 moveDir;
    float currTime = 0;
    bool previouslyFloor = true;
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
        var collision = controller.Move(input * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ground") && previouslyFloor && !speakersDestroyed)
        {
            speaker.clip = muffledMusic;
            speaker.time = currTime;
            speaker.volume = 1f;
            speaker.Play();
            crowdSFX.Stop();
            previouslyFloor = false;
        }

        if (hit.gameObject.CompareTag("Floor") && !previouslyFloor && !speakersDestroyed)
        {
            speaker.clip = normalMusic;
            speaker.time = currTime;
            speaker.volume = .3f;
            speaker.Play();
            crowdSFX.Play();
            previouslyFloor = true;
        }

        if (hit.gameObject.CompareTag("Floor") && !previouslyFloor && speakersDestroyed)
        {
            speaker.Stop();
            crowdSFX.Play();
        }

        if (hit.gameObject.CompareTag("Ground") && previouslyFloor && speakersDestroyed)
        {
            speaker.Stop();
            crowdSFX.Stop();
        }
    }
}
