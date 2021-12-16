using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject playerTwoD;
    public GameObject playerThreeD;

    public GameObject targetObject;
    private float targetAngle = 0;
    const float rotationAmount = 1.5f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;

    public GameObject TwoDWorld;
    public GameObject ThreeDWorld;

    bool twoDActive = true;

    private Space offsetPositionSpace = Space.Self;

    public Vector3 cameraOffset2d;
    public Vector3 cameraOffset3d;
    bool cameraOffset3dSet = false;


    public float smoothFactor = 0.5f;
    public bool lookAtTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset2d = transform.position - targetObject.transform.position;
        gameManager = FindObjectOfType<GameManager>();
        targetObject = GameObject.Find("PlayerTwoDModel");

        changeVisibility(ThreeDWorld, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isTwoD())
        {
            targetObject = GameObject.Find("PlayerTwoDModel");
        }
        else
        {
            targetObject = GameObject.Find("PlayerThreeDModel");
        }
            

        // Trigger functions if Rotate is requested
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gameManager.isTwoD())
        {
            targetAngle -= 90.0f;

            //twoDActive = false;
            gameManager.setisTwoDActive(false);
            playerTwoD.SetActive(false);
            playerThreeD.SetActive(true);
            //GetChildRecursive(ThreeDWorld);
            //GetChildRecursive(TwoDWorld);
            //changeVisibility(playerTwoD, false);
            //changeVisibility(playerThreeD, true);
            changeVisibility(ThreeDWorld, true);
            changeVisibility(TwoDWorld, false);
            //ThreeDWorld.SetActive(true);
            //TwoDWorld.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !gameManager.isTwoD())
        {
            targetAngle += 90.0f;
            //twoDActive = true;
            gameManager.setisTwoDActive(true);
            playerTwoD.SetActive(true);
            playerThreeD.SetActive(false);
            //GetChildRecursive(ThreeDWorld);
            //GetChildRecursive(TwoDWorld);
            //changeVisibility(playerTwoD, true);
            //changeVisibility(playerThreeD, false);
            changeVisibility(ThreeDWorld, false);
            changeVisibility(TwoDWorld, true);
            //ThreeDWorld.SetActive(false);
            //TwoDWorld.SetActive(true);
        }


        if (targetAngle != 0)
        {
            Rotate();
        }
    }

    private void changeVisibility(GameObject obj, bool isVisible)
    {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            print(child.gameObject.GetComponent<Renderer>().enabled);
            child.gameObject.GetComponent<Renderer>().enabled = isVisible;

            //child.gameObject.GetComponent<Renderer>().SetActive(showMeshRenderer);


            changeVisibility(child.gameObject, isVisible);
        }
    }


    void LateUpdate()
    {
        if (gameManager.isTwoD())
        {
            Vector3 newPosition = targetObject.transform.position + cameraOffset2d;
            //transform.position = newPosition;
            transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
            

        }
        else 
        {

            Vector3 newPosition = targetObject.transform.position + new Vector3(-7.0f, 1.5f, 0.0f);
            //transform.position = newPosition;
            transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
            /*
            transform.position = target.position + offset;
            transform.LookAt(target.position);
            */
        }

        

    }
    
   


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
