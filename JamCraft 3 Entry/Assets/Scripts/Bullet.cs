﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public int damage = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9) //Boundaries Layer
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 10) //Enemy Layer
        {
            EnemyHealth enemyHealthScript = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealthScript.health -= damage;
            Destroy(gameObject);
        }
    }
}
