using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float timeUntilExplosion = 2f;
    private bool hasExploded = false;

    public float radius = 5f;
    public float force = 700f;

    public GameObject explosionEffect;

    private float countdown;
    BloodSplatter bloodsplatter;

    void Awake()
    {
        bloodsplatter = GameObject.FindWithTag("BloodParticleSystem").GetComponent<BloodSplatter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        countdown = timeUntilExplosion;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && hasExploded == false)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider [] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            EnemyMovement EM = nearbyObject.gameObject.GetComponent<EnemyMovement>();
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            ImpactReceiver IR = nearbyObject.GetComponent<ImpactReceiver>();

            if (rb != null) //Add explosion force to rigidbody objects
            {
                float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);

                if (distance <= 4)
                {
                    if (EM != null)
                    {
                        StartCoroutine(EM.ChangeIsKinematic());
                    }                 
                    rb.AddExplosionForce(force, transform.position, radius);
                }
                else
                {
                    if (EM != null)
                    {
                        StartCoroutine(EM.ChangeIsKinematic());
                    }
                    rb.AddExplosionForce((force / 1.5f), transform.position, radius);
                }             
            }
            if (IR != null) //Add explosion force to CharacterController objects
            {
                Vector3 dir = nearbyObject.transform.position - transform.position;
                float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);

                if (distance <= 4)
                {
                    IR.AddImpact(dir, (force / 8));
                }
                else
                {
                    IR.AddImpact(dir, (force / 10));
                }
            }


            if (nearbyObject.tag == "Player") //If the object's tag is a player, deal some damage depending on the player's distance to the explosion.
            {
                DealExplosionDamage(nearbyObject);
            }
            if (nearbyObject.tag == "Enemy") //If the object's tag is an enemy, deal some damage depending on the enemy's distance to the explosion.
            {
                EnemyHealth enemyHealthScript = nearbyObject.GetComponent<EnemyHealth>();

                if (enemyHealthScript != null)
                {
                    DealExplosionDamage(nearbyObject, enemyHealthScript);
                }            
            }
        }

        Destroy(gameObject);
    }

    //Explosion damage calculations for a character.
    void DealExplosionDamage(Collider nearbyObject, EnemyHealth healthScript = null)
    {
        float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);

        //Player Calculations
        if (healthScript == null)
        {
            if (distance > 4.5f)
            {
                PlayerHealth.health -= 25;
            }
            else if (distance <= 4.5f && distance > 4)
            {
                PlayerHealth.health -= 50;
            }
            else if (distance <= 4 && distance > 3.5f)
            {
                PlayerHealth.health -= 75;
            }
            else if (distance <= 3.5f)
            {
                PlayerHealth.health -= 100;
            }
        }

        //Enemy calculations
        else
        {
            if (distance > 4.5f)
            {
                healthScript.health -= 25;
            }
            else if (distance <= 4.5f && distance > 4)
            {
                healthScript.health -= 50;
            }
            else if (distance <= 4 && distance > 3.5f)
            {
                healthScript.health -= 75;
            }
            else if (distance <= 3.5f)
            {
                healthScript.health -= 100;
            }
        }

        //Blood Particles
        bloodsplatter.DoBloodSplatter(nearbyObject.gameObject.transform);
    }
}
