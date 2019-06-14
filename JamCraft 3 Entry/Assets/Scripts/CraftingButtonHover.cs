using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingButtonHover : MonoBehaviour
{
    private string macheteInfo;
    public TextMeshProUGUI craftingInfo;
    private string buttonName;

    private Inventory inv;
    private SlotSelection SS;
    private WorkbenchCrafting WC;

    void Awake()
    {
        WC = GameObject.FindGameObjectWithTag("Interact").GetComponent<WorkbenchCrafting>();
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    public void OnMouseHover()
    {
        buttonName = gameObject.name.ToString();

        if (buttonName == "MacheteButton")
        {
            craftingInfo.text = "Crafting Requirements: 3 Machete Parts";
        }
        if (buttonName == "PistolButton")
        {
            craftingInfo.text = "Crafting Requirements: 4 Pistol Parts";
        }
    }

    public void OnMouseExit()
    {
        craftingInfo.text = "Crafting Requirements:";
        buttonName = "";
    }

    public void OnClick()
    {
        if (buttonName == "MacheteButton")
        {
            if (inv.canCraftMachete == true)
            {
                SS.hasMachete = true;
                inv.macheteParts -= 3;
                WC.ShowCraftingResult("Success", "Machete");
            }
            else
            {
                WC.ShowCraftingResult("Fail");
            }
        }
        if (buttonName == "PistolButton")
        {
            if (inv.canCraftPistol == true)
            {
                SS.hasPistol = true;
                inv.pistolParts -= 4;
                WC.ShowCraftingResult("Success", "Pistol");
            }
            else
            {
                WC.ShowCraftingResult("Fail");
            }
        }
    }
}
