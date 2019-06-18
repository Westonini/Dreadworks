using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseConsumableItem : MonoBehaviour
{
    public Rigidbody pipebombPrefab;
    public Transform pipebombThrowPosition;

    public float gauzeHealTime = 3f;

    public TextMeshProUGUI healingText;

    public static bool playerIsHealing = false;

    SlotSelection SS;
    Inventory inv;
    PlayerController PC;

    void Awake()
    {
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    void Update()
    {
        //If the player presses Fire1 button and has the pipebomb equipped.
        if (Input.GetButtonDown("Fire1") && SS.pipebomb.activeSelf == true)
        {
            ThrowPipebomb();
        }

        //If the player holds Fire1 button and has the gauze equipped.
        if (Input.GetButton("Fire1") && SS.gauze.activeSelf == true && PlayerHealth.health < 100)
        {
            if (playerIsHealing == false)
            {
                StartCoroutine("UseHealItem");
            }           
        }
        if (Input.GetButtonUp("Fire1") && SS.gauze.activeSelf == true && playerIsHealing == true)
        {
            StopHeal();
        }
    }

    void ThrowPipebomb()
    {
        Rigidbody pipebombInstance;
        pipebombInstance = Instantiate(pipebombPrefab, pipebombThrowPosition.position, pipebombThrowPosition.rotation) as Rigidbody;
        pipebombInstance.AddForce(pipebombThrowPosition.forward * 100);
        pipebombInstance.AddForce(pipebombThrowPosition.up * 100);

        inv.pipebombCount -= 1;

        if (inv.pipebombCount == 0)
        {
            SS.ChangeSlots(SS.nothingEquipped);
        }
    }

    private IEnumerator UseHealItem()
    {
        healingText.text = "Healing...";
        playerIsHealing = true;
        PC.movementSpeed = PC.sneakingSpeed;

        yield return new WaitForSeconds(gauzeHealTime);

        healingText.text = "";
        playerIsHealing = false;
        PC.movementSpeed = PC.resetMovementSpeed;

        PlayerHealth.health += 50;

        inv.gauzeCount -= 1;

        if (inv.gauzeCount == 0)
        {
            SS.ChangeSlots(SS.nothingEquipped);
        }
    }

    public void StopHeal()
    {
        healingText.text = "";
        PC.movementSpeed = PC.resetMovementSpeed;
        StopCoroutine("UseHealItem");
        playerIsHealing = false;
    }
}
