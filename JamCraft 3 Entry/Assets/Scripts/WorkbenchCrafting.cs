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
        inventoryContents.text = "Ammo: " + inv.ammo.ToString() + "\n" +
                                 "Machete Parts: " + inv.macheteParts.ToString() + "\n" +
                                 "Pistol Parts: " + inv.pistolParts.ToString() + "\n" +
                                 "Bullet Casings: " + inv.bulletCasings.ToString() + "\n" +
                                 "Gunpowder: " + inv.gunpowder.ToString() + "\n";
    }

    public void ShowCraftingResult(string Result, string item = null)
    {
        if (Result == "Success")
        {
            craftingResult.color = new Color32(0, 255, 0, 150);
            craftingResult.text = item + " successfully crafted.";
            StartCoroutine(ClearCraftingResultText());
        }
        else
        {
            craftingResult.color = new Color32(255, 0, 0, 150);
            craftingResult.text = "Crafting requirements not met.";
            StartCoroutine(ClearCraftingResultText());
        }
    }

    private IEnumerator ClearCraftingResultText()
    {
        yield return new WaitForSeconds(3f);
        craftingResult.text = "";
    }
}
