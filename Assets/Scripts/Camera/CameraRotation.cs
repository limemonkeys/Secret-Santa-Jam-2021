using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject playerTwoD;
    public GameObject playerThreeD;
    public GameObject TempObsticals2d;
    public GameObject TempObsticals3d;
    public GameObject Presents2d;
    public GameObject Presents3d;

    public GameObject MovementControls2d;
    public GameObject MovementControls3d;
    public GameObject SwitchPerspectiveLeft;
    public GameObject SwitchPerspectiveRight;

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
        targetObject = GameObject.Find("PlayerTwoD");
        GameObject.Find("PlayerThreeDModel").GetComponent<Renderer>().enabled = false;
        changeVisibility(Presents3d, false); ;
        //Presents3d.SetActive(false);

        changeVisibility(ThreeDWorld, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isCanMove() && gameManager.isCanRotate())
        {
            if (gameManager.isTwoD())
            {
                targetObject = GameObject.Find("PlayerTwoD");
            }
            else
            {
                targetObject = GameObject.Find("PlayerThreeD");
            }


            // Trigger functions if Rotate is requested
            if (Input.GetKeyDown(KeyCode.LeftArrow) && gameManager.isTwoD())
            {
                targetAngle -= 90.0f;
                gameManager.setisTwoDActive(false);

                TempObsticals2d.SetActive(false);
                TempObsticals3d.SetActive(false);

                changeVisibility(Presents2d, false);
                changeVisibility(Presents3d, true);
                //Presents2d.SetActive(false);
                //Presents3d.SetActive(true);

                MovementControls2d.SetActive(false);
                MovementControls3d.SetActive(true);
                SwitchPerspectiveLeft.SetActive(false);
                SwitchPerspectiveRight.SetActive(true);


                Vector3 playerTwoDPos = playerTwoD.transform.position;
                Vector3 playerThreeDPos = playerThreeD.transform.position;
                playerThreeD.transform.position = new Vector3(playerTwoDPos.x, playerTwoDPos.y, playerThreeDPos.z);

                GameObject.Find("PlayerTwoDModel").GetComponent<Renderer>().enabled = false;
                //playerTwoD.GetComponent<Collider>().enabled = false;

                GameObject.Find("PlayerThreeDModel").GetComponent<Renderer>().enabled = true;
                //playerThreeD.GetComponent<Collider>().enabled = true;

                changeVisibility(ThreeDWorld, true);
                changeVisibility(TwoDWorld, false);


                print("Switch1");


            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !gameManager.isTwoD())
            {
                targetAngle += 90.0f;
                gameManager.setisTwoDActive(true);

                TempObsticals2d.SetActive(true);
                TempObsticals3d.SetActive(true);

                changeVisibility(Presents2d, true);
                changeVisibility(Presents3d, false);
                //Presents2d.SetActive(true);
                //Presents3d.SetActive(false);

                MovementControls2d.SetActive(true);
                MovementControls3d.SetActive(false);
                SwitchPerspectiveLeft.SetActive(true);
                SwitchPerspectiveRight.SetActive(false);

                Vector3 playerTwoDPos = playerTwoD.transform.position;
                Vector3 playerThreeDPos = playerThreeD.transform.position;
                playerTwoD.transform.position = new Vector3(playerThreeDPos.x, playerThreeDPos.y, -5f);

                GameObject.Find("PlayerTwoDModel").GetComponent<Renderer>().enabled = true;
                //playerTwoD.GetComponent<Collider>().enabled = true;

                GameObject.Find("PlayerThreeDModel").GetComponent<Renderer>().enabled = false;
                //playerThreeD.GetComponent<Collider>().enabled = false;

                changeVisibility(ThreeDWorld, false);
                changeVisibility(TwoDWorld, true);

                print("Switch2");

            }


            if (targetAngle != 0)
            {
                Rotate();
            }
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
            print(child.gameObject.name);
            print(child.gameObject.tag);
            if (child.gameObject.tag == "DoNotRender")
            {
            }
            else
            {
                child.gameObject.GetComponent<Renderer>().enabled = isVisible;
            }



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
