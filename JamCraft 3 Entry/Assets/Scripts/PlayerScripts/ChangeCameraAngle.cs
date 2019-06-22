using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraAngle : MonoBehaviour
{
    public GameObject virtualCam;

    private string angle = "0";
    public static string angleBeingSwitchedTo = "0";

    public float cameraRotateSpeed = 5;

    private bool currentlyRotating = false;
    private float currentlyRotatingTimer = 0.8f;
    private float resetTimer;

    private bool rotateRight = false;
    private bool rotateLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        angle = "0";
        angleBeingSwitchedTo = "0";
        resetTimer = currentlyRotatingTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TurnCameraRight") && !currentlyRotating)
        {
            currentlyRotating = true;
            rotateRight = true;
        }

        if (Input.GetButtonDown("TurnCameraLeft") && !currentlyRotating)
        {
            currentlyRotating = true;
            rotateLeft = true;
        }

        if (rotateRight == true)
        {
            if (angle == "0")
            {
                virtualCam.transform.rotation = Quaternion.Slerp(virtualCam.transform.rotation, Quaternion.Euler(45, 90, 0), Time.deltaTime * cameraRotateSpeed);
                angleBeingSwitchedTo = "90";
                if (currentlyRotating == false)
                {
                    virtualCam.transform.rotation = Quaternion.Euler(45, 90, 0);
                    angle = "90";
                    rotateRight = false;
                }
            }
            else if (angle == "90")
            {
                virtualCam.transform.rotation = Quaternion.Slerp(virtualCam.transform.rotation, Quaternion.Euler(45, 180, 0), Time.deltaTime * cameraRotateSpeed);
                angleBeingSwitchedTo = "180";
                if (currentlyRotating == false)
                {
                    virtualCam.transform.rotation = Quaternion.Euler(45, 180, 0);
                    angle = "180";
                    rotateRight = false;
                }
            }
            else if (angle == "180")
            {
                virtualCam.transform.rotation = Quaternion.Slerp(virtualCam.transform.rotation, Quaternion.Euler(45, 270, 0), Time.deltaTime * cameraRotateSpeed);
                angleBeingSwitchedTo = "270";
                if (currentlyRotating == false)
                {
                    virtualCam.transform.rotation = Quaternion.Euler(45, 270, 0);
                    angle = "270";
                    rotateRight = false;
                }
            }
            else if (angle == "270")
            {
                virtualCam.transform.rotation = Quaternion.Slerp(virtualCam.transform.rotation, Quaternion.Euler(45, 0, 0), Time.deltaTime * cameraRotateSpeed);
                angleBeingSwitchedTo = "0";
                if (currentlyRotating == false)
                {
                    virtualCam.transform.rotation = Quaternion.Euler(45, 0, 0);
                    angle = "0";
                    rotateRight = false;
                }
            }
        }

        if (rotateLeft == true)
        {
            if (angle == "0")
            {
                virtualCam.transform.rotation = Quaternion.Slerp(virtualCam.transform.rotation, Quaternion.Euler(45, 270, 0), Time.deltaTime * cameraRotateSpeed);
                angleBeingSwitchedTo = "270";
                if (currentlyRotating == false)
                {
                    virtualCam.transform.rotation = Quaternion.Euler(45, 270, 0);
                    angle = "270";
                    rotateLeft = false;
                }
            }
            else if (angle == "270")
            {
                virtualCam.transform.rotation = Quaternion.Slerp(virtualCam.transform.rotation, Quaternion.Euler(45, 180, 0), Time.deltaTime * cameraRotateSpeed);
                angleBeingSwitchedTo = "180";
                if (currentlyRotating == false)
                {
                    virtualCam.transform.rotation = Quaternion.Euler(45, 180, 0);
                    angle = "180";
                    rotateLeft = false;
                }
            }
            else if (angle == "180")
            {
                virtualCam.transform.rotation = Quaternion.Slerp(virtualCam.transform.rotation, Quaternion.Euler(45, 90, 0), Time.deltaTime * cameraRotateSpeed);
                angleBeingSwitchedTo = "90";
                if (currentlyRotating == false)
                {
                    virtualCam.transform.rotation = Quaternion.Euler(45, 90, 0);
                    angle = "90";
                    rotateLeft = false;
                }
            }
            else if (angle == "90")
            {
                virtualCam.transform.rotation = Quaternion.Slerp(virtualCam.transform.rotation, Quaternion.Euler(45, 0, 0), Time.deltaTime * cameraRotateSpeed);
                angleBeingSwitchedTo = "0";
                if (currentlyRotating == false)
                {
                    virtualCam.transform.rotation = Quaternion.Euler(45, 0, 0);
                    angle = "0";
                    rotateLeft = false;
                }
            }
        }

        if (currentlyRotating == true)
        {
            currentlyRotatingTimer -= Time.deltaTime;

            if (currentlyRotatingTimer <= 0)
            {
                currentlyRotatingTimer = resetTimer;
                currentlyRotating = false;
            }
        }
    }
}
