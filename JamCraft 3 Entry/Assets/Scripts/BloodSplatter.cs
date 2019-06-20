using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    public GameObject bloodSplatter;
    public ParticleSystem bloodParticles;

    public void DoBloodSplatter(Transform splatterposition)
    {
        splatterposition.position = new Vector3(splatterposition.transform.position.x, splatterposition.transform.position.y + 0.9f, splatterposition.transform.position.z);
        GameObject bloodSplatterInstance;
        bloodSplatterInstance = Instantiate(bloodSplatter, splatterposition.position, splatterposition.rotation) as GameObject;
        Destroy(bloodSplatterInstance.gameObject, 3.25f);
    }
}
