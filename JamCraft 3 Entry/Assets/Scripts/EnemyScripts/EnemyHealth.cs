using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject enemyRagdoll;

    [HideInInspector]
    public int maxHealth;

    void Start()
    {
        maxHealth = health;
    }

    void Update()
    {
        if (health <= 0)
        {
            GameObject ragdollInstance;
            ragdollInstance = Instantiate(enemyRagdoll, transform.position, transform.rotation) as GameObject;

            Destroy(gameObject);
        }
    }
}
