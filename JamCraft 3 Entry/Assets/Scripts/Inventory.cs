using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public bool canCraftPistol = false, canCraftMachete = false, canCraftAmmo = false, canCraftPipebomb = false, canCraftGauze = false;

    [HideInInspector]
    public int pistolParts, macheteParts, ammo, bulletCasings, gunpowder, fuses, cloth;

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
        canCraftPistol = (pistolParts >= 4) ? true : false;
        canCraftMachete = (macheteParts >= 3) ? true : false;
        canCraftAmmo = (bulletCasings >= 6 && gunpowder >= 3) ? true : false;
        canCraftPipebomb = (fuses >= 2 && gunpowder >= 2) ? true : false;
        canCraftGauze = (cloth >= 4) ? true : false;

        SS.hasPipebomb = (pipebombCount >= 1) ? true : false;
        SS.hasGauze = (gauzeCount >= 1) ? true : false;
    }
}
