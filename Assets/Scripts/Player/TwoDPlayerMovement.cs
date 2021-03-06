using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDPlayerMovement : MonoBehaviour
{
    private GameManager gameManager;

    CharacterController characterController;

    public float jumpSpeed = 20.0f;
    public float gravity = 5.0f;
    public float speed = 5.0f;

    private Vector3 moveDirection = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        var vertical = 0;
        var horizontal = 0;

        // If the 2d player is in thier native dimension (2d)
        if (gameManager.isTwoD())
        {
            if (Input.GetKey(KeyCode.D))
            {
                horizontal = 1;
            }


            if (Input.GetKey(KeyCode.A))
            {
                horizontal = -1;
            }

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
        else 
        {
            if (Input.GetKey(KeyCode.W))
            {
                horizontal = 1;
            }


            if (Input.GetKey(KeyCode.S))
            {
                horizontal = -1;
            }
            
        }
    }
}
