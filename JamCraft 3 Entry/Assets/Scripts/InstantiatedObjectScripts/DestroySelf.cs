using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroySelf : MonoBehaviour
{
    public float destroyDelay = 0.5f;

    public bool explodeOnSpawn = false;
    public float explosionRadius = 5.0F;
    public float explosionPower = 10.0F;

    Cinemachine.CinemachineVirtualCamera c_VirtualCamera;

    void Awake()
    {
        c_VirtualCamera = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    void Start()
    {
        if (gameObject.name != "InstantiatedRagdollPlayer(Clone)")
        {
            Destroy(gameObject, destroyDelay);
        }

        if (explodeOnSpawn)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.tag == "Ragdoll")
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 3.0F);
                    }
                }
            }
        }

        if (gameObject.name == "InstantiatedRagdollPlayer(Clone)")
        {
            FindObjectOfType<AudioManager>().Stop("Walking");
            FindObjectOfType<AudioManager>().Stop("Sneaking");
            FindObjectOfType<AudioManager>().Stop("HeavyBreathing");
            FindObjectOfType<AudioManager>().Stop("Heartbeat");
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            c_VirtualCamera.m_LookAt = gameObject.transform;
            c_VirtualCamera.m_Follow = gameObject.transform;

            Invoke("RestartLevel", 7.5f);
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
