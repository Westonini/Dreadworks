using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseKey : MonoBehaviour
{
    private bool isTouchingLockedDoor = false;
    public float unlockTime = 1.5f;
    private float unlockTimeReset;
    private bool unlocking;

    private bool gotTheScripts = false;

    public TextMeshProUGUI unlockText;

    private GameObject lockedDoor;
    private GameObject unlockedDoor;

    SlotSelection SS;
    Inventory inv;
    EnableOrDisableScripts EODS;

    void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
        EODS = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnableOrDisableScripts>();
    }

    void Start()
    {
        unlockTimeReset = unlockTime;
    }

    void Update()
    {
        Unlock();

        //If the player presses Fire1 or Interact button and has the key equipped next to a locked door.
        if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Interact")) && SS.key.activeSelf && isTouchingLockedDoor && !unlocking)
        {
            unlocking = true;
        }
        //Cancel unlock
        else if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Interact")) && SS.key.activeSelf && isTouchingLockedDoor && unlocking)
        {
            EndUnlock(true);
        }

        //Cancel unlock
        if (!isTouchingLockedDoor && unlocking)
        {
            EndUnlock(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is touching a LockedDoor object.
        if (other.gameObject.tag == "LockedDoor")
        {
            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).transform.name == "LockedDoor")
                {
                    lockedDoor = other.transform.GetChild(i).transform.gameObject;
                }
                if (other.transform.GetChild(i).transform.name == "UnlockedDoor")
                {
                    unlockedDoor = other.transform.GetChild(i).transform.gameObject;
                }
            }

            isTouchingLockedDoor = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //If player is touching a LockedDoor object.
        if (other.gameObject.tag == "LockedDoor")
        {
            if (isTouchingLockedDoor == false)
            {
                for (int i = 0; i < other.transform.childCount; i++)
                {
                    if (other.transform.GetChild(i).transform.name == "LockedDoor")
                    {
                        lockedDoor = other.transform.GetChild(i).transform.gameObject;
                    }
                    if (other.transform.GetChild(i).transform.name == "UnlockedDoor")
                    {
                        unlockedDoor = other.transform.GetChild(i).transform.gameObject;
                    }
                }

                isTouchingLockedDoor = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //If player was touching a LockedDoor object.
        if (other.gameObject.tag == "LockedDoor")
        {
            isTouchingLockedDoor = false;
        }
    }

    void Unlock()
    {
        if (unlocking == true)
        {
            StartUnlock();

            if (unlockTime <= 0)
            {
                EndUnlock();
            }
        }
    }

    void StartUnlock() //Start the unlock
    {
        unlockTime -= Time.deltaTime;
        unlockText.text = "Unlocking...\n" + "Press SPACE to cancel";

        if (gotTheScripts == false)
        {
            EODS.GetScripts();
            EODS.EnableDisableScripts(false);
            gotTheScripts = true;
        }
    }

    void EndUnlock(bool EndedEarly = false) //End the unlock
    {
        EODS.EnableDisableScripts(true);
        unlockText.text = "";
        unlocking = false;
        unlockTime = unlockTimeReset;
        gotTheScripts = false;
        //FindObjectOfType<AudioManager>().Stop("Searching");

        if (!EndedEarly)
        {
            UnlockDoor();
            //FindObjectOfType<AudioManager>().Play("Jingle");
        }
    }

    void UnlockDoor() //Called when the player successfully finishes unlocking a door
    {
        inv.keysCount -= 1;
        SS.ChangeSlots(SS.nothingEquipped);
        lockedDoor.SetActive(false);
        unlockedDoor.SetActive(true);

        isTouchingLockedDoor = false;
    }
}
