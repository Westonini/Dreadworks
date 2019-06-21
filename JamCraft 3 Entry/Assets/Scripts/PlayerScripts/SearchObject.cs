﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchObject : MonoBehaviour
{
    private bool isTouchingItemProvider = false;

    public float searchTime = 3f;
    private float searchTimeReset;
    private bool searching = false;
    public TextMeshProUGUI searchText;
    public TextMeshProUGUI searchResultsText;

    private ItemProvider IP;
    private PlayerController PC;
    private PlayerShooting PS;
    private PlayerMelee PM;
    private SlotSelection SS;
    private UseConsumableItem UCI;

    private Inventory inv;

    private bool gotTheScripts = false;

    private GameObject closedObject;
    private GameObject openedObject;

    void Awake()
    {
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void Start()
    {
        searchTimeReset = searchTime;
    }

    void Update()
    {
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
            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).transform.name == "Closed")
                {
                    closedObject = other.transform.GetChild(i).transform.gameObject;
                }
                if (other.transform.GetChild(i).transform.name == "Open")
                {
                    openedObject = other.transform.GetChild(i).transform.gameObject;
                }
            }
            IP = other.gameObject.GetComponent<ItemProvider>();
            isTouchingItemProvider = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //If player is touching an ItemProvider object.
        if (other.gameObject.tag == "ItemProvider")
        {
            if (!isTouchingItemProvider)
            {
                for (int i = 0; i <= other.transform.childCount; i++)
                {
                    if (other.transform.GetChild(i).transform.name == "Closed")
                    {
                        closedObject = other.transform.GetChild(i).transform.gameObject;
                    }
                    if (other.transform.GetChild(i).transform.name == "Open")
                    {
                        openedObject = other.transform.GetChild(i).transform.gameObject;
                    }
                }
                IP = other.gameObject.GetComponent<ItemProvider>();
                isTouchingItemProvider = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //If player was touching an ItemProvider object.
        if (other.gameObject.tag == "ItemProvider")
        {
            closedObject = null;
            openedObject = null;
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
        CancelInvoke("ClearSearchResults");
        ClearSearchResults();

        foreach (string i in IP.itemsToGive)
        {
            if (i == "PistolPart")
            {
                inv.pistolParts += 1;
                searchResultsText.text += " + 1 Pistol Part \n";
            }
            if (i == "MachetePart")
            {
                inv.macheteParts += 1;
                searchResultsText.text += " + 1 Machete Part \n";
            }
            if (i == "Ammo")
            {
                int amountToGive = Random.Range(3, 6);
                inv.ammo += amountToGive;
                searchResultsText.text += " + " + amountToGive.ToString() + " Ammo \n";
            }
            if (i == "BulletCasings")
            {
                int amountToGive = Random.Range(4, 7);
                inv.bulletCasings += amountToGive;
                searchResultsText.text += " + " + amountToGive.ToString() + " Bullet Casings \n";
            }
            if (i == "Gunpowder")
            {
                int amountToGive = Random.Range(2, 4);
                inv.gunpowder += amountToGive;
                searchResultsText.text += " + " + amountToGive.ToString() + " Gunpowder \n";
            }
            if (i == "Pipebomb")
            {
                inv.pipebombCount += 1;
                searchResultsText.text += " + 1 Pipebomb \n";
            }
            if (i == "Fuses")
            {
                int amountToGive = Random.Range(1, 3);
                inv.fuses += amountToGive;
                searchResultsText.text += " + " + amountToGive.ToString() + " Fuses \n";
            }
            if (i == "Gauze")
            {
                inv.gauzeCount += 1;
                searchResultsText.text += " + 1 Gauze \n";
            }
            if (i == "Cloth")
            {
                int amountToGive = Random.Range(2, 4);
                inv.cloth += amountToGive;
                searchResultsText.text += " + " + amountToGive.ToString() + " Cloth \n";
            }
            if (i == "KeyFragments")
            {
                int amountToGive = 1;
                inv.keyFragments += amountToGive;
                searchResultsText.text += " + " + amountToGive.ToString() + " Key Fragment \n";
            }
        }

        Invoke("ClearSearchResults", 4.5f);
        IP.checkedThisProvider = true;
        
    }

    public void EnableDisableScripts(bool TF) //true to enable, false to disable.
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
        if (UCI != null)
        {
            UCI.enabled = TF;
        }
        if (SS != null)
        {
            SS.enabled = TF;
        }
    }

    public void GetScripts() //get script components
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
            UCI = GameObject.Find("Player").GetComponent<UseConsumableItem>();
            if (UCI != null)
            {
                UCI.StopHeal();
            }
        }
        catch
        {
            UCI = null;
        }

        try
        {
            SS = GameObject.FindWithTag("Player").GetComponent<SlotSelection>();
            if (PS != null)
            {
                SS.CancelReload();
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
        searchText.text = "Searching...\n" + "Press SPACE to cancel";
        closedObject.SetActive(false);
        openedObject.SetActive(true);

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
        searchTime = searchTimeReset;
        gotTheScripts = false;

        if (EndedEarly == false)
        {
            SearchResults();
        }
        else
        {
            closedObject.SetActive(true);
            openedObject.SetActive(false);
        }
    }

    void ClearSearchResults()
    {
        searchResultsText.text = "";
    }
}
