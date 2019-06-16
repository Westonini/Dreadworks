using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseConsumableItem : MonoBehaviour
{
    SlotSelection SS;
    Inventory inv;

    void Awake()
    {
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1") && SS.pipebomb.activeSelf == true)
        {
            //Throw pipebomb
            //Remove 1 pipebomb from inventory's pipebomb count
            //change slot to nothingequipped.
        }
    }
}
