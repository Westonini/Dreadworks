using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    private bool flashlightIsOn = true;
    public GameObject flashlight;
    public GameObject playerLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Toggle flashlight
        if (Input.GetButtonDown("Flashlight") && flashlightIsOn == true)
        {
            flashlight.SetActive(false);
            playerLight.SetActive(false);
            flashlightIsOn = false;
        }
        else if (Input.GetButtonDown("Flashlight") && flashlightIsOn == false)
        {
            flashlight.SetActive(true);
            playerLight.SetActive(true);
            flashlightIsOn = true;
        }
    }
}
