using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 10f;
    public float gravity = 9.81f;

    CharacterController controller;
    Vector3 input;
    Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        input = (transform.right * moveHoriz + transform.forward * moveVert).normalized;
        input *= playerSpeed;

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
}
