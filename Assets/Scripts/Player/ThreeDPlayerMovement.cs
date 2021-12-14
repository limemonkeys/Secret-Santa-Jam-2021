using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDPlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    public float jumpSpeed = 20.0f;
    public float gravity = 5.0f;
    public float speed = 5.0f;

    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        var vertical = 0;
        var horizontal = 0;

        if (Input.GetKey(KeyCode.W))
        {
            horizontal = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            vertical = -1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            horizontal = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            vertical = 1;
        }
        /*
        transform.Translate(new Vector3(horizontal, 0, -vertical) * (speed * Time.deltaTime));


        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(horizontal, 0.0f, -vertical);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        */


        float yStore = moveDirection.y;

        moveDirection = (transform.forward * -vertical) + (transform.right * horizontal);
        moveDirection = moveDirection.normalized * speed;

        moveDirection.y = yStore;


        if (characterController.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravity * Time.deltaTime);
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
