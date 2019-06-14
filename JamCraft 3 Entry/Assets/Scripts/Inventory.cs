using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public bool canCraftPistol = false, canCraftMachete = false, canCraftAmmo = false;

    [HideInInspector]
    public int pistolParts, macheteParts, ammo, bulletCasings, gunpowder;

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
    }
}
