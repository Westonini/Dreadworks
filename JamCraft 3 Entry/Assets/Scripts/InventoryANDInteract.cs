using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryANDInteract : MonoBehaviour
{
    public bool canCraftPistol = false;
    public bool canCraftMachete = false;
    private int pistolParts;
    private int macheteParts;
    public int ammo;

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

    void OnTriggerStay(Collider other)
    {
        //Interact with a ItemProvider
        if (Input.GetButtonDown("Interact") && other.gameObject.tag == "ItemProvider")
        {
            ItemProvider IP = other.gameObject.GetComponent<ItemProvider>();

            if (IP.checkedThisProvider == false)
            {
                foreach (string i in IP.itemsToGive)
                {
                    if (i == "PistolPart")
                    {
                        pistolParts += 1;
                    }
                    if (i == "MachetePart")
                    {
                        macheteParts += 1;
                    }
                    if (i == "Ammo")
                    {
                        ammo += 6;
                    }
                }

                IP.checkedThisProvider = true;
            }
        }
    }
}
