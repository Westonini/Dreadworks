using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    [HideInInspector]
    public bool flashlightIsOn = true;
    public GameObject flashlight;
    public GameObject playerLight;

    // Update is called once per frame
    void Update()
    {      
        if (Input.GetButtonDown("Flashlight") && flashlightIsOn == true)
        {
            ToggleFlashLight(false);
        }
        else if (Input.GetButtonDown("Flashlight") && flashlightIsOn == false)
        {
            ToggleFlashLight(true);
        }
    }

    //Toggle flashlight
    public void ToggleFlashLight(bool Toggle)
    {
        if (flashlight != null)
        {
            flashlight.SetActive(Toggle);
            playerLight.SetActive(Toggle);
            flashlightIsOn = Toggle;
        }
    }
}
