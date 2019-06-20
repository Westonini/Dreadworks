using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public int damage = 25;
    public int knockback = 250;

    BloodSplatter bloodsplatter;
    EnemyMovement EM;

    void Awake()
    {
        bloodsplatter = GameObject.FindWithTag("BloodParticleSystem").GetComponent<BloodSplatter>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == 12) //Boundaries and Ground Layers
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 10) //Enemy Layer
        {
            //Deal Damage
            EnemyHealth enemyHealthScript = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealthScript.health -= damage;

            //Knockback
            EnemyMovement EM = other.gameObject.GetComponent<EnemyMovement>();
            EM.Knockback(knockback, gameObject, true);

            //BloodParticles
            bloodsplatter.DoBloodSplatter(other.gameObject.transform);

            //Destroy self (instantiated bullet)
            Destroy(gameObject);
        }
    }
}
