using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryANDInteract : MonoBehaviour
{
    private bool isTouchingItemProvider = false;

    [HideInInspector]
    public bool canCraftPistol = false;
    [HideInInspector]
    public bool canCraftMachete = false;
    private int pistolParts;
    private int macheteParts;
    [HideInInspector]
    public int ammo;

    private float searchTime = 2f;
    private bool searching = false;
    public TextMeshPro searchText;

    private ItemProvider IP;
    private PlayerController PC;
    private PlayerShooting PS;
    private PlayerMelee PM;
    private SlotSelection SS;

    private bool gotTheScripts = false;

    void Awake()
    {
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
    }

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

        Search(); //Called when searching is set to true

        if (isTouchingItemProvider == true && Input.GetButtonDown("Interact") && searching == false) //Begin search
        {
            if (IP.checkedThisProvider == false)
            {
                searching = true;
            }
        }
        else if (isTouchingItemProvider == true && Input.GetButtonDown("Interact") && searching == true) //Cancel search
        {
            EndSearch(true);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        //If player is touching an ItemProvider object.
        if (other.gameObject.tag == "ItemProvider")
        {
            IP = other.gameObject.GetComponent<ItemProvider>();
            isTouchingItemProvider = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //If player was touching an ItemProvider object.
        if (other.gameObject.tag == "ItemProvider")
        {
            IP = null;
            isTouchingItemProvider = false;
        }
    }

    void Search() //called when player searches something.
    {
        if (searching == true)
        {
            StartSearch();

            if (searchTime <= 0)
            {
                EndSearch();
            }
        }
    }

    void SearchResults() //Called when the player successfully finishes searching something
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

    void EnableDisableScripts(bool TF) //true to enable, false to disable.
    {
        if (PC != null)
        {
            PC.enabled = TF;
        }
        if (PS != null)
        {
            PS.enabled = TF;
        }
        if (PM != null)
        {
            PM.enabled = TF;
        }
        if (SS != null)
        {
            SS.enabled = TF;
        } 
    }

    void GetScripts() //get script components
    {
        try
        {
            PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
        catch
        {
            PC = null;
        }

        try
        {
            PS = GameObject.Find("Pistol").GetComponent<PlayerShooting>();
        }
        catch
        {
            PM = null;
        }

        try
        {
            PM = GameObject.Find("Machete").GetComponent<PlayerMelee>();
        }
        catch
        {
            PM = null;
        }
        
        
        try
        {
            SS = GameObject.FindWithTag("Player").GetComponent<SlotSelection>();
            if (PS != null)
            {
                SS.CancelReload();
                PS.CancelReloadAndMuzzleFlash();
            }
        }
        catch
        {
            SS = null;
        }
    }

    void StartSearch() //Begin the search
    {
        searchTime -= Time.deltaTime;
        searchText.text = "Searching...\n" + "Press E again to cancel";

        if (gotTheScripts == false)
        {
            GetScripts();
            EnableDisableScripts(false);
            gotTheScripts = true;
        }
    }

    void EndSearch(bool EndedEarly = false) //End the search
    {
        EnableDisableScripts(true);
        searchText.text = "";
        searching = false;
        searchTime = 2f;
        gotTheScripts = false;

        if (EndedEarly == false)
        {
            SearchResults();
        }     
    }
}
