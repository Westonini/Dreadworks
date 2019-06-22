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
    private AudioSource[] enemyHurtSound;

    BloodSplatter bloodsplatter;
    EnemyHealth EH;
    EnemyDetectionMovement EDM;
    Transform enemyLocation;
    GameObject enemy;

    private bool isWithinMeleeRange = false;

    public int knockback = 40;

    private Animator macheteAnim;

    void Awake()
    {
        bloodsplatter = GameObject.FindWithTag("BloodParticleSystem").GetComponent<BloodSplatter>();
        macheteAnim = GetComponent<Animator>();
    }

    void Start()
    {
        meleeCooldownReset = meleeCooldown;
    }

    void OnEnable()
    {
        macheteAnim.SetBool("isAttacking", false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !meleeIsOnCooldown && isWithinMeleeRange && enemy != null) //Attack if melee isnt on cooldown and there's an enemy within range.
        {
            DoMeleeHit();
        }
        if (Input.GetButtonDown("Fire1") && !meleeIsOnCooldown) //If the player presses fire key and melee isnt on cooldown.
        {
            meleeIsOnCooldown = true;

            //SoundEffect
            FindObjectOfType<AudioManager>().Play("MeleeSwing");

            //Animation
            macheteAnim.SetBool("isAttacking", true);
        }


        if (meleeIsOnCooldown == true) //melee cooldown timer so it's not spammable.
        {
            meleeCooldown -= Time.deltaTime;

            if (meleeCooldown <= 0)
            {
                macheteAnim.SetBool("isAttacking", false);
                meleeIsOnCooldown = false;
                meleeCooldown = meleeCooldownReset;
            }
        }

        //If the enemy is dead, call SetInfoToNull()
        if (enemy == null)
        {
            SetInfoToNull();
        }

        macheteAnim.keepAnimatorControllerStateOnDisable = true;
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
        EDM.Knockback(knockback, gameObject);

        //BloodParticles
        bloodsplatter.DoBloodSplatter(enemyLocation);

        //Hurt Sound (has to be the second audio source in the enemy's inspector).
        enemyHurtSound[1].Play();
    }

    //Takes info from the enemy while it's within trigger range.
    void GetInfo(Collider other, bool reset = false)
    {
        if (!reset)
        {
            isWithinMeleeRange = true;

            enemy = other.gameObject;
            EH = enemy.GetComponent<EnemyHealth>();
            EDM = enemy.GetComponent<EnemyDetectionMovement>();
            enemyLocation = enemy.transform;
            enemyHurtSound = enemy.GetComponents<AudioSource>();
        }
        else
        {
            SetInfoToNull();
        }
    }

    //Used when the enemy leaves the melee trigger area or if the enemy is dead.
    void SetInfoToNull()
    {
        isWithinMeleeRange = false;

        enemy = null;
        EH = null;
        EDM = null;
        enemyLocation = null;
        enemyHurtSound = null;
    }
}
