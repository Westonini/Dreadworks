using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunfireDetection : MonoBehaviour
{
    EnemyMovement EM;
    PlayerShooting PS;
    SlotSelection SS;

    void Awake()
    {      
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
        EM = gameObject.GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is touching the gunfireDetectionRange object and shoots the pistol.
        if (other.gameObject.tag == "Player" && SS.currentlySelectedItem == "Pistol" && Input.GetButtonDown("Fire1"))
        {
            PS = GameObject.FindGameObjectWithTag("Pistol").GetComponent<PlayerShooting>();

            if (PS.bulletsInMag != 0 && PS.reloadTimeActive == false)
            {
                EM.DetectedPlayer();
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
                EM.DetectedPlayer();
            }
        }
    }
}
