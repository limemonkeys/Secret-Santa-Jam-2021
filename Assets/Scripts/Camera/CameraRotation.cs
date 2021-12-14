using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject targetObject;
    private float targetAngle = 0;
    const float rotationAmount = 1.5f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;

    public GameObject TwoDWorld;
    public GameObject ThreeDWorld;

    bool twoDActive = true;

    private Space offsetPositionSpace = Space.Self;

    public Vector3 cameraOffset;


    public float smoothFactor = 0.5f;
    public bool lookAtTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // Trigger functions if Rotate is requested
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gameManager.isTwoD())
        {
            targetAngle -= 90.0f;

            //twoDActive = false;
            gameManager.setisTwoDActive(false);
            ThreeDWorld.SetActive(true);
            TwoDWorld.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !gameManager.isTwoD())
        {
            targetAngle += 90.0f;
            //twoDActive = true;
            gameManager.setisTwoDActive(true);
            ThreeDWorld.SetActive(false);
            TwoDWorld.SetActive(true);
        }


        if (targetAngle != 0)
        {
            Rotate();
        }

        

    }

    /*
    void LateUpdate()
    {
        Vector3 newPosition = targetObject.transform.position + cameraOffset;
        transform.position = newPosition;
        //transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
    }
    */

    protected void Rotate()
    {

        float step = rSpeed * Time.deltaTime;
        float orbitCircumfrance = 2F * rDistance * Mathf.PI;
        float distanceDegrees = (rSpeed / orbitCircumfrance) * 360;
        float distanceRadians = (rSpeed / orbitCircumfrance) * 2 * Mathf.PI;

        if (targetAngle > 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, -rotationAmount);
            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, rotationAmount);
            targetAngle += rotationAmount;
        }

    }
}
