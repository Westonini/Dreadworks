using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloserDetection : MonoBehaviour
{
    private EnemyDetectionMovement EDM;

    void Awake()
    {
        EDM = gameObject.GetComponentInParent<EnemyDetectionMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is touching the closerDetectionRange object.
        if (other.gameObject.tag == "Player")
        {
            EDM.isWithinDetectionRange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //If player is touching the closerDetectionRange object.
        if (other.gameObject.tag == "Player")
        {
            if (EDM.isWithinDetectionRange == false)
            {
                EDM.isWithinDetectionRange = true;
            }
        }
    }
}
