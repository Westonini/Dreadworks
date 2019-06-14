using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public bool canCraftPistol = false, canCraftMachete;

    [HideInInspector]
    public int pistolParts, macheteParts, ammo;

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
    }
}
