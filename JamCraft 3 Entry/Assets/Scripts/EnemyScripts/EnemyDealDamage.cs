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

    BloodSplatter bloodsplatter;
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
        if (other.gameObject.tag == "Player" && meleeIsOnCooldown == false) //If player is within range of the triggercollider...
        {
            DealDamage(other);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && meleeIsOnCooldown == false) //If player is within range of the triggercollider...
        {
            DealDamage(other);
        }
    }

    void DealDamage(Collider other)
    {
        //Deal Damage
        PlayerHealth.health -= damage;
        meleeIsOnCooldown = true;

        //Knockback
        ImpactReceiver IR = other.GetComponent<ImpactReceiver>();
        Vector3 direction = (other.transform.position - transform.position).normalized;
        IR.AddImpact(direction, knockbackForce);

        //BloodParticles
        bloodsplatter.DoBloodSplatter(other.gameObject.transform);
    }
}
