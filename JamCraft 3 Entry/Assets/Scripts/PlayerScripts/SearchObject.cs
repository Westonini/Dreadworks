using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchObject : MonoBehaviour
{
    private bool isTouchingItemProvider = false;

    public float searchTime = 3f;
    private float searchTimeReset;
    [HideInInspector]
    public bool searching = false;
    public TextMeshProUGUI searchText;
    public TextMeshProUGUI searchResultsText;

    private ItemProvider IP;
    private Inventory inv;
    private EnableOrDisableScripts EODS;

    private bool gotTheScripts = false;

    private GameObject closedObject;
    private GameObject openedObject;

    void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        EODS = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnableOrDisableScripts>();
    }

    void Start()
    {
        searchTimeReset = searchTime;
    }

    void Update()
    {
        Search(); //Called when searching is set to true

        //Begin search
        if (isTouchingItemProvider == true && Input.GetButtonDown("Interact") && searching == false)
        {
            if (IP.checkedThisProvider == false)
            {
                searching = true;
                FindObjectOfType<AudioManager>().Play("Searching");
            }
        }
        //Cancel search
        else if (isTouchingItemProvider == true && Input.GetButtonDown("Interact") && searching == true)
        {
            EndSearch(true);
        }
        //Cancel search
        else if (isTouchingItemProvider == false && searching == true)
        {
            EndSearch(true);
            IP = null;
            closedObject = null;
            openedObject = null;
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

    void StartSearch() //Begin the search
    {
        searchTime -= Time.deltaTime;
        searchText.text = "Searching...\n" + "Press SPACE to cancel";
        closedObject.SetActive(false);
        openedObject.SetActive(true);

        if (gotTheScripts == false)
        {
            EODS.GetScripts();
            EODS.EnableDisableScripts(false);
            gotTheScripts = true;
        }
    }

    void EndSearch(bool EndedEarly = false) //End the search
    {
        EODS.EnableDisableScripts(true);
        searchText.text = "";
        searching = false;
        searchTime = searchTimeReset;
        gotTheScripts = false;
        FindObjectOfType<AudioManager>().Stop("Searching");

        if (!EndedEarly)
        {
            SearchResults();
            FindObjectOfType<AudioManager>().Play("Jingle");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Close");
            closedObject.SetActive(true);
            openedObject.SetActive(false);
        }
    }

    void ClearSearchResults()
    {
        searchResultsText.text = "";
    }
}
