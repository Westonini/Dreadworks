using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private EnemyMovement EM;

    void Awake()
    {
        EM = gameObject.GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is touching the detectionRange object.
        if (other.gameObject.tag == "Player" && !PlayerController.playerIsSneaking)
        {
            EM.isWithinDetectionRange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //If player is touching the detectionRange object.
        if (other.gameObject.tag == "Player" && !PlayerController.playerIsSneaking)
        {
            if (EM.isWithinDetectionRange == false)
            {
                EM.isWithinDetectionRange = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //If player was touching the detectionRange object.
        if (other.gameObject.tag == "Player")
        {
            EM.isWithinDetectionRange = false;
        }
    }
}
