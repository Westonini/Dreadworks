using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsLevelReset : MonoBehaviour
{
    Pause pause;

    private void Awake()
    {
        pause = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Pause>();
    }

    //If player touches this object the scene resets
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pause.Retry();
        }
    }
}
