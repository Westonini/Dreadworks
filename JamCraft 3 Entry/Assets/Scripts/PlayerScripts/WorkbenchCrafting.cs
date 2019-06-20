using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkbenchCrafting : MonoBehaviour
{
    private bool isTouchingWorkBench = false;
    private bool crafting = false;

    public GameObject craftingMenu;
    public TextMeshProUGUI inventoryContents;
    public TextMeshProUGUI craftingResult;

    private Inventory inv;
    private SearchObject SO;

    void Awake()
    {
        SO = GameObject.FindGameObjectWithTag("Interact").GetComponent<SearchObject>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        if (isTouchingWorkBench == true && Input.GetButtonDown("Interact") && crafting == false) //Open crafting window
        {
            crafting = true;
            craftingMenu.SetActive(true);
        }
        else if (isTouchingWorkBench == true && (Input.GetButtonDown("Interact") || Input.GetKeyDown(KeyCode.Escape)) && crafting == true) //Close crafting window
        {
            crafting = false;
            craftingMenu.SetActive(false);
        }

        //If player is crafting, freeze player.
        if (crafting == true)
        {
            SO.GetScripts();
            SO.EnableDisableScripts(false);
            UpdateInventoryContentText();
        }
        else if (crafting == false && isTouchingWorkBench == true)
        {
            SO.EnableDisableScripts(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is touching an ItemProvider object.
        if (other.gameObject.tag == "Workbench")
        {
            isTouchingWorkBench = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //If player is touching an ItemProvider object.
        if (other.gameObject.tag == "Workbench")
        {
            if (isTouchingWorkBench == false)
            {
                isTouchingWorkBench = true;
            }          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //If player was touching an ItemProvider object.
        if (other.gameObject.tag == "Workbench")
        {
            isTouchingWorkBench = false;
        }
    }

    void UpdateInventoryContentText()
    {
        inventoryContents.text = "Pipebombs: " + inv.pipebombCount.ToString() + "\n" +
                                 "Gauze: " + inv.gauzeCount.ToString() + "\n" +
                                 "Ammo: " + inv.ammo.ToString() + "\n" +
                                 "Keys: " + inv.keys.ToString() + "\n" +
                                 "\n" +
                                 "Machete Parts: " + inv.macheteParts.ToString() + "\n" +
                                 "Pistol Parts: " + inv.pistolParts.ToString() + "\n" +
                                 "Key Fragments: " + inv.keyFragments.ToString() + "\n" +
                                 "\n" +
                                 "Gunpowder: " + inv.gunpowder.ToString() + "\n" +
                                 "Bullet Casings: " + inv.bulletCasings.ToString() + "\n" +
                                 "Fuses: " + inv.fuses.ToString() + "\n" +
                                 "Cloth: " + inv.cloth.ToString() + "\n";
    }

    public void ShowCraftingResult(string Result, string item = null)
    {
        CancelInvoke("ClearCraftingResultText");

        if (Result == "Success")
        {
            craftingResult.color = new Color32(0, 255, 0, 150);
            craftingResult.text = item + " successfully crafted.";
            Invoke("ClearCraftingResultText", 3f);
        }
        else
        {
            craftingResult.color = new Color32(255, 0, 0, 150);
            craftingResult.text = "Crafting requirements not met.";
            Invoke("ClearCraftingResultText", 3f);
        }
    }

    private void ClearCraftingResultText()
    {
        craftingResult.text = "";
    }
}
