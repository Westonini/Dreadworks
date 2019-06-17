using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseConsumableItem : MonoBehaviour
{
    public Rigidbody pipebombPrefab;
    public Transform pipebombThrowPosition;

    SlotSelection SS;
    Inventory inv;

    void Awake()
    {
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }


    void Update()
    {
        //If the player presses Fire1 button and has the pipebomb equipped.
        if (Input.GetButtonDown("Fire1") && SS.pipebomb.activeSelf == true)
        {
            ThrowPipebomb();
        }
        //If the player presses Fire1 button and has the gauze equipped.
        if (Input.GetButton("Fire1") && SS.gauze.activeSelf == true)
        {
            //UseHealItem();
            //Heals over x amount of seconds while button is being held
            //Makes character move at sneak movement speed
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
}
