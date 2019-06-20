using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private EnemyDetectionMovement EDM;

    void Awake()
    {
        EDM = gameObject.GetComponentInParent<EnemyDetectionMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is touching the detectionRange object.
        if (other.gameObject.tag == "Player" && !PlayerController.playerIsSneaking)
        {
            EDM.isWithinDetectionRange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //If player is touching the detectionRange object.
        if (other.gameObject.tag == "Player" && !PlayerController.playerIsSneaking)
        {
            if (EDM.isWithinDetectionRange == false)
            {
                EDM.isWithinDetectionRange = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //If player was touching the detectionRange object.
        if (other.gameObject.tag == "Player")
        {
            EDM.isWithinDetectionRange = false;
        }
    }
}
