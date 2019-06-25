using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsLevelReset : MonoBehaviour
{
    //If player or enemy touches this, they die.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth.health = 0;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            EnemyHealth EH = other.gameObject.GetComponent<EnemyHealth>();
            EH.health = 0;
        }
    }
}
