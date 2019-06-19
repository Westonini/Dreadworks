using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloserDetection : MonoBehaviour
{
    private EnemyMovement EM;

    void Awake()
    {
        EM = gameObject.GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is touching the closerDetectionRange object.
        if (other.gameObject.tag == "Player")
        {
            EM.isWithinDetectionRange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //If player is touching the closerDetectionRange object.
        if (other.gameObject.tag == "Player")
        {
            if (EM.isWithinDetectionRange == false)
            {
                EM.isWithinDetectionRange = true;
            }
        }
    }
}
