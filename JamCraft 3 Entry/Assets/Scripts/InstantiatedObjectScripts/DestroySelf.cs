using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float destroyDelay = 0.5f;

    public bool explodeOnSpawn = false;
    public float explosionRadius = 5.0F;
    public float explosionPower = 10.0F;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDelay);

        if (explodeOnSpawn)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.tag == "EnemyRagdoll")
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 3.0F);
                    }
                }
            }
        }
    }
}
