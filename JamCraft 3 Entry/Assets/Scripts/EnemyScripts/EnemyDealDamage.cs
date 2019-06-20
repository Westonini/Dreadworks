using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public int damage = 10;
    public float meleeCooldown = 0.5f;
    private float meleeCooldownReset;
    private bool meleeIsOnCooldown = false;

    public int knockbackForce = 10;

    void Start()
    {
        meleeCooldownReset = meleeCooldown;
    }

    void Update()
    {
        if (meleeIsOnCooldown == true) //melee cooldown timer so it's not spammable.
        {
            meleeCooldown -= Time.deltaTime;

            if (meleeCooldown <= 0)
            {
                meleeIsOnCooldown = false;
                meleeCooldown = meleeCooldownReset;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && meleeIsOnCooldown == false) //If player presses fire key, an enemy is within the collider, and melee is not on cooldown.
        {
            PlayerHealth.health -= damage;
            meleeIsOnCooldown = true;

            //Knockback
            ImpactReceiver IR = other.GetComponent<ImpactReceiver>();
            Vector3 direction = (other.transform.position - transform.position).normalized;
            IR.AddImpact(direction, knockbackForce);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && meleeIsOnCooldown == false) //If player presses fire key, an enemy is within the collider, and melee is not on cooldown.
        {
            PlayerHealth.health -= damage;
            meleeIsOnCooldown = true;

            //Knockback
            ImpactReceiver IR = other.GetComponent<ImpactReceiver>();
            Vector3 direction = (other.transform.position - transform.position).normalized;
            IR.AddImpact(direction, knockbackForce);
        }
    }
}
