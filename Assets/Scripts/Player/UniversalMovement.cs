using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalMovement : MonoBehaviour
{
    CharacterController characterController2d;
    CharacterController characterController3d;

    private GameManager gameManager;

    public float jumpSpeed = 20.0f;
    public float gravity = 5.0f;
    public float speed = 5.0f;

    private Vector3 moveDirection2d = Vector3.zero;
    private Vector3 moveDirection3d = Vector3.zero;

    private Vector3 respawnPoint2d;
    private Vector3 respawnPoint3d;

    public AudioSource jumpSFX;

    // Start is called before the first frame update
    void Start()
    {
        characterController2d = GameObject.Find("PlayerTwoD").GetComponent<CharacterController>();
        characterController3d = GameObject.Find("PlayerThreeD").GetComponent<CharacterController>();

        respawnPoint2d = GameObject.Find("PlayerTwoD").transform.position;
        respawnPoint3d = GameObject.Find("PlayerThreeD").transform.position;

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isCanMove()) 
        {
            if (Input.GetKeyDown(KeyCode.R) && gameManager.isCanRotate())
            {
                Respawn();
            }
            if (gameManager.isTwoD())
            {
                var horizontal = 0;
                var vertical = 0;

                if (Input.GetKey(KeyCode.D))
                {
                    horizontal = 1;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    horizontal = -1;
                }

                float yStore2d = moveDirection2d.y;
                float yStore3d = moveDirection2d.y;

                moveDirection2d = (transform.forward * -vertical) + (transform.right * horizontal);
                moveDirection2d = moveDirection2d.normalized * speed;
                moveDirection3d = (transform.forward * -vertical) + (transform.right * horizontal);
                moveDirection3d = moveDirection3d.normalized * speed;

                moveDirection2d.y = yStore2d;
                moveDirection3d.y = yStore3d;

                if (characterController2d.isGrounded)
                {
                    moveDirection2d.y = 0f;
                    if (Input.GetButtonDown("Jump"))
                    {
                        jumpSFX.Play();
                        
                        
                        moveDirection2d.y = jumpSpeed;
                    }
                }

                if (characterController3d.isGrounded)
                {
                    moveDirection3d.y = 0f;
                    if (Input.GetButtonDown("Jump"))
                    {
                        moveDirection3d.y = jumpSpeed;
                    }
                }

                moveDirection2d.y = moveDirection2d.y + (Physics.gravity.y * gravity * Time.deltaTime);
                moveDirection3d.y = moveDirection3d.y + (Physics.gravity.y * gravity * Time.deltaTime);

                characterController2d.Move(moveDirection2d * Time.deltaTime);
                characterController3d.Move(moveDirection3d * Time.deltaTime);

            }
            else
            {
                var horizontal = 0;
                var vertical = 0;

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

                float yStore2d = moveDirection2d.y;
                float yStore3d = moveDirection2d.y;

                moveDirection2d = (transform.forward * 0.0f) + (transform.right * horizontal);
                moveDirection2d = moveDirection2d.normalized * speed;
                moveDirection3d = (transform.forward * -vertical) + (transform.right * horizontal);
                moveDirection3d = moveDirection3d.normalized * speed;

                moveDirection2d.y = yStore2d;
                moveDirection3d.y = yStore3d;

                if (characterController2d.isGrounded)
                {
                    moveDirection2d.y = 0f;
                    if (Input.GetButtonDown("Jump"))
                    {
                        moveDirection2d.y = jumpSpeed;
                    }
                }

                if (characterController3d.isGrounded)
                {
                    
                    moveDirection3d.y = 0f;
                    if (Input.GetButtonDown("Jump"))
                    {
                        jumpSFX.Play();
                        moveDirection3d.y = jumpSpeed;
                    }
                }

                moveDirection2d.y = moveDirection2d.y + (Physics.gravity.y * gravity * Time.deltaTime);
                moveDirection3d.y = moveDirection3d.y + (Physics.gravity.y * gravity * Time.deltaTime);

                characterController2d.Move(moveDirection2d * Time.deltaTime);
                characterController3d.Move(moveDirection3d * Time.deltaTime);
            }
        }
    }
    public void Respawn() 
    {
        GameObject.Find("PlayerTwoD").transform.position = respawnPoint2d;
        GameObject.Find("PlayerThreeD").transform.position = respawnPoint3d;

    }
}
