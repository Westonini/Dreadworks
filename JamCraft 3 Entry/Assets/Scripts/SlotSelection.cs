using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSelection : MonoBehaviour
{
    public bool hasMachete = false;
    public bool hasPistol = false;

    public GameObject nothingEquipped;
    public GameObject machete;
    public GameObject pistol;

    private string currentlySelectedItem;

    private PlayerShooting PS;

    [HideInInspector]
    public bool cancelReload;

    void Awake()
    {
        PS = pistol.GetComponent<PlayerShooting>();
    }

    void Update()
    {
        //Machete is slot 1
        if (Input.GetButtonDown("Slot1") && hasMachete == true && currentlySelectedItem != "Machete")
        {
            ChangeSlots(machete);
        }
        else if (Input.GetButtonDown("Slot1") && hasMachete == true && currentlySelectedItem == "Machete")
        {
            ChangeSlots(nothingEquipped);
        }

        //Pistol is slot 2
        if (Input.GetButtonDown("Slot2") && hasPistol == true && currentlySelectedItem != "Pistol")
        {
            ChangeSlots(pistol);
        }
        else if (Input.GetButtonDown("Slot2") && hasPistol == true && currentlySelectedItem == "Pistol")
        {
            ChangeSlots(nothingEquipped);
        }
    }

    //Changes currently equipped item to whatever is passed in.
    void ChangeSlots(GameObject selectedSlot)
    {
        if (pistol.activeSelf == true && currentlySelectedItem == "Pistol")
        {
            CancelReload();
        }

        nothingEquipped.SetActive(false);
        machete.SetActive(false);
        pistol.SetActive(false);

        selectedSlot.SetActive(true);
        currentlySelectedItem = selectedSlot.name.ToString();
    }

    public void CancelReload()
    {
        PS.ammoText.text = "";
        PS.reloadText.text = "";
        PS.muzzleFlashLight.SetActive(false);
        PS.muzzleFlashParticles.Stop();

        PS.CancelInvoke("Reload");
        PS.StopCoroutine(PS.ToggleMuzzleFlash());
        PS.reloadTimeActive = false;

        //cancelReload = true;
    }
}
