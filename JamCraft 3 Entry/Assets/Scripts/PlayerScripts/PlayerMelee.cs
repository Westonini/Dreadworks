using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMelee : MonoBehaviour
{
    public int damage = 35;
    public float meleeCooldown = 0.5f;
    private float meleeCooldownReset;
    private bool meleeIsOnCooldown = false;

    BloodSplatter bloodsplatter;

    public int knockback = 40;

    void Awake()
    {
        bloodsplatter = GameObject.FindWithTag("BloodParticleSystem").GetComponent<BloodSplatter>();
    }

    void Start()
    {
        meleeCooldownReset = meleeCooldown;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && meleeIsOnCooldown == false) //If the player presses fire key and melee isnt on cooldown.
        {
            meleeIsOnCooldown = true;
        }

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
        if (Input.GetButtonDown("Fire1") && other.gameObject.tag == "Enemy" && meleeIsOnCooldown == false) //If player presses fire key, an enemy is within the collider, and melee is not on cooldown.
        {
            DoMeleeHit(other);
        }
    }
    void OnTriggerStay(Collider other) 
    {
        if (Input.GetButtonDown("Fire1") && other.gameObject.tag == "Enemy" && meleeIsOnCooldown == false) //If player presses fire key, an enemy is within the collider, and melee is not on cooldown.
        {
            DoMeleeHit(other);
        }
    }

    void DoMeleeHit(Collider other)
    {
        //Deal Damage
        EnemyHealth enemyHealthScript = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealthScript.health -= damage;

        //Knockback
        EnemyMovement EM = other.gameObject.GetComponent<EnemyMovement>();
        EM.Knockback(knockback, gameObject);

        //BloodParticles
        bloodsplatter.DoBloodSplatter(other.gameObject.transform);
    }
}
