using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool isTouchingUnlockedDoor = false;

    private GameObject unlockedDoor;
    private GameObject openedDoor;

    private Collider doorCollider;

    void Update()
    {
        if (isTouchingUnlockedDoor == true && Input.GetButtonDown("Interact") && unlockedDoor.activeSelf)
        {
            FindObjectOfType<AudioManager>().Play("Open");
            doorCollider.enabled = false;
            unlockedDoor.SetActive(false);
            openedDoor.SetActive(true);

            isTouchingUnlockedDoor = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is touching an ItemProvider object.
        if (other.gameObject.tag == "LockedDoor")
        {
            doorCollider = other.gameObject.GetComponent<Collider>();

            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).transform.name == "UnlockedDoor")
                {
                    unlockedDoor = other.transform.GetChild(i).transform.gameObject;
                }
                if (other.transform.GetChild(i).transform.name == "UnlockedDoorOpened")
                {
                    openedDoor = other.transform.GetChild(i).transform.gameObject;
                }
            }

            isTouchingUnlockedDoor = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //If player is touching an ItemProvider object.
        if (other.gameObject.tag == "LockedDoor")
        {
            doorCollider = other.gameObject.GetComponent<Collider>();

            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).transform.name == "UnlockedDoor")
                {
                    unlockedDoor = other.transform.GetChild(i).transform.gameObject;
                }
                if (other.transform.GetChild(i).transform.name == "UnlockedDoorOpened")
                {
                    openedDoor = other.transform.GetChild(i).transform.gameObject;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //If player is touching an ItemProvider object.
        if (other.gameObject.tag == "LockedDoor")
        {
            isTouchingUnlockedDoor = false;
        }
    }
}
