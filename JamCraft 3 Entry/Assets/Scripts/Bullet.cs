using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public int damage = 25;
    public int knockback = 250;
    private EnemyMovement EM;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == 12) //Boundaries and Ground Layers
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 10) //Enemy Layer
        {
            EnemyHealth enemyHealthScript = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealthScript.health -= damage;

            //Knockback
            EnemyMovement EM = other.gameObject.GetComponent<EnemyMovement>();
            EM.Knockback(knockback, gameObject, true);

            Destroy(gameObject);
        }
    }
}
