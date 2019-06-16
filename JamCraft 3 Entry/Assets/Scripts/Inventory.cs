using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public bool canCraftPistol = false, canCraftMachete = false, canCraftAmmo = false;

    [HideInInspector]
    public int pistolParts, macheteParts, ammo, bulletCasings, gunpowder;

    [HideInInspector]
    public int pipebombCount = 0, gauzeCount = 0;

    private SlotSelection SS;

    void Awake()
    {
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pistolParts == 4)
        {
            canCraftPistol = true;
        }
        if (macheteParts == 3)
        {
            canCraftMachete = true;
        }
        if (bulletCasings >= 6 && gunpowder >= 4)
        {
            canCraftAmmo = true;
        }


        if (pipebombCount >= 1)
        {
            SS.hasPipebomb = true;
        }
        if (gauzeCount >= 1)
        {
            SS.hasGauze = true;
        }
    }
}
