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
    EnemyHealth EH;
    EnemyMovement EM;
    Transform enemyLocation;

    private bool isWithinMeleeRange = false;

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
        if (Input.GetButtonDown("Fire1") && meleeIsOnCooldown == false && isWithinMeleeRange)
        {
            DoMeleeHit();
        }
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
        if (other.gameObject.tag == "Enemy") //If player presses fire key, an enemy is within the collider, and melee is not on cooldown.
        {
            GetInfo(other);
        }
    }
    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Enemy") //If player presses fire key, an enemy is within the collider, and melee is not on cooldown.
        {
            GetInfo(other);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetInfo(other, true);
        }
    }

    //Deals damage, knockback, and instantiates blood particles
    void DoMeleeHit()
    {
        //Deal Damage
        EH.health -= damage;

        //Knockback
        EM.Knockback(knockback, gameObject);

        //BloodParticles
        bloodsplatter.DoBloodSplatter(enemyLocation);
    }

    //Takes info from the enemy while it's within trigger range.
    void GetInfo(Collider other, bool reset = false)
    {
        if (!reset)
        {
            isWithinMeleeRange = true;

            EH = other.gameObject.GetComponent<EnemyHealth>();
            EM = other.gameObject.GetComponent<EnemyMovement>();
            enemyLocation = other.gameObject.transform;
        }
        else
        {
            isWithinMeleeRange = false;

            EH = null;
            EM = null;
            enemyLocation = null;
        }


    }
}
