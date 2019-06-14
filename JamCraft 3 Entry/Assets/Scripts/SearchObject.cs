using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchObject : MonoBehaviour
{
    private bool isTouchingItemProvider = false;

    public float searchTime = 5f;
    private float searchTimeReset;
    private bool searching = false;
    public TextMeshPro searchText;
    public TextMeshProUGUI searchResultsText;

    private ItemProvider IP;
    private PlayerController PC;
    private PlayerShooting PS;
    private PlayerMelee PM;
    private SlotSelection SS;

    private Inventory inv;

    private bool gotTheScripts = false;

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
        else if (isTouchingItemProvider == true && (Input.GetButtonDown("Interact") || Input.GetKeyDown(KeyCode.Escape)) && searching == true) //Cancel search
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
                inv.pistolParts += 1;
                searchResultsText.text += " + Pistol Part \n";
            }
            if (i == "MachetePart")
            {
                inv.macheteParts += 1;
                searchResultsText.text += " + Machete Part \n";
            }
            if (i == "Ammo")
            {
                inv.ammo += IP.ammoToBeGiven;
                searchResultsText.text += " + Ammo \n";
            }
        }

        Invoke("ClearSearchResults", 2.5f);
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
        searchTime = searchTimeReset;
        gotTheScripts = false;

        if (EndedEarly == false)
        {
            SearchResults();
        }     
    }

    void ClearSearchResults()
    {
        searchResultsText.text = "";
    }
}
