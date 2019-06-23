using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    public GameObject bloodSplatter;
    public ParticleSystem bloodParticles;

    public void DoBloodSplatter(Transform positionToInstantiate)
    {
        //Offset the positionToInstantiate on the y-axis
        Transform splatterPos = transform;
        splatterPos.position = new Vector3(positionToInstantiate.transform.position.x, positionToInstantiate.transform.position.y + 0.9f, positionToInstantiate.transform.position.z);

        //Create an instance of the bloodSplatter to be instantiated
        GameObject bloodSplatterInstance;
        bloodSplatterInstance = Instantiate(bloodSplatter, splatterPos.position, splatterPos.rotation) as GameObject;

        //Destroy the instantiated bloodSplatter after 3.25 seconds
        Destroy(bloodSplatterInstance.gameObject, 3.25f);
    }
}
