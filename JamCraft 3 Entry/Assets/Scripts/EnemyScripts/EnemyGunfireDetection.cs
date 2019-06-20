using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunfireDetection : MonoBehaviour
{
    EnemyDetectionMovement EDM;
    PlayerShooting PS;
    SlotSelection SS;

    void Awake()
    {      
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
        EDM = gameObject.GetComponentInParent<EnemyDetectionMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is touching the gunfireDetectionRange object and shoots the pistol.
        if (other.gameObject.tag == "Player" && SS.currentlySelectedItem == "Pistol" && Input.GetButtonDown("Fire1"))
        {
            PS = GameObject.FindGameObjectWithTag("Pistol").GetComponent<PlayerShooting>();

            if (PS.bulletsInMag != 0 && PS.reloadTimeActive == false)
            {
                EDM.isWithinDetectionRange = true;
            }          
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //If player is touching the gunfireDetectionRange object and shoots the pistol.
        if (other.gameObject.tag == "Player" && SS.currentlySelectedItem == "Pistol" && Input.GetButtonDown("Fire1"))
        {
            PS = GameObject.FindGameObjectWithTag("Pistol").GetComponent<PlayerShooting>();

            if (PS.bulletsInMag != 0 && PS.reloadTimeActive == false)
            {
                EDM.isWithinDetectionRange = true;
            }
        }
    }
}
