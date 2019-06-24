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
        //Sets buttonName to whichever button the mouse is hovering over and then sets craftingInfo text accordingly
        buttonName = gameObject.name.ToString();

        if (buttonName == "MacheteButton")
        {
            craftingInfo.text = "Crafting Requirements: 3 Machete Parts";
        }
        if (buttonName == "PistolButton")
        {
            craftingInfo.text = "Crafting Requirements: 4 Pistol Parts";
        }
        if (buttonName == "AmmoButton")
        {
            craftingInfo.text = "Crafting Requirements: 6 Bullet Casings & 3 Gunpowder";
        }
        if (buttonName == "PipebombButton")
        {
            craftingInfo.text = "Crafting Requirements: 2 Fuses & 2 Gunpowder";
        }
        if (buttonName == "GauzeButton")
        {
            craftingInfo.text = "Crafting Requirements: 4 Cloth";
        }
        if (buttonName == "KeyButton")
        {
            craftingInfo.text = "Crafting Requirements: 4 Key Fragments";
        }
    }

    public void OnMouseExit()
    {
        craftingInfo.text = "Crafting Requirements:";
        buttonName = "";
    }

    public void OnClick()
    {
        //Craft if player has all the materials.
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
        if (buttonName == "AmmoButton")
        {
            if (inv.canCraftAmmo == true)
            {
                inv.bulletCasings -= 6;
                inv.gunpowder -= 3;
                inv.ammo += 6;
                WC.ShowCraftingResult("Success", "6 Ammo");
            }
            else
            {
                WC.ShowCraftingResult("Fail");
            }
        }
        if (buttonName == "PipebombButton")
        {
            if (inv.canCraftPipebomb == true)
            {
                inv.fuses -= 2;
                inv.gunpowder -= 2;
                inv.pipebombCount += 1;
                WC.ShowCraftingResult("Success", "Pipebomb");
            }
            else
            {
                WC.ShowCraftingResult("Fail");
            }
        }
        if (buttonName == "GauzeButton")
        {
            if (inv.canCraftGauze == true)
            {
                inv.cloth -= 4;
                inv.gauzeCount += 1;
                WC.ShowCraftingResult("Success", "Gauze");
            }
            else
            {
                WC.ShowCraftingResult("Fail");
            }
        }
        if (buttonName == "KeyButton")
        {
            if (inv.canCraftKey == true)
            {
                inv.keyFragments -= 4;
                inv.keysCount += 1;
                WC.ShowCraftingResult("Success", "Key");
            }
            else
            {
                WC.ShowCraftingResult("Fail");
            }
        }
    }
}
