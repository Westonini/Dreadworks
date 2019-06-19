using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSelection : MonoBehaviour
{
    public bool hasMachete = false;
    public bool hasPistol = false;
    public bool hasPipebomb = false;
    public bool hasGauze = false;

    public GameObject nothingEquipped;
    public GameObject machete;
    public GameObject pistol;
    public GameObject pipebomb;
    public GameObject gauze;

    [HideInInspector]
    public string currentlySelectedItem = "NoWeapon";

    private PlayerShooting PS;
    private UseConsumableItem UCI;

    [HideInInspector]
    public bool cancelReload;

    void Awake()
    {
        PS = pistol.GetComponent<PlayerShooting>();
        UCI = GameObject.FindGameObjectWithTag("Player").GetComponent<UseConsumableItem>();
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

        //Pipebomb is slot 3
        if (Input.GetButtonDown("Slot3") && hasPipebomb == true && currentlySelectedItem != "Pipebomb")
        {
            ChangeSlots(pipebomb);
        }
        else if (Input.GetButtonDown("Slot3") && hasPipebomb == true && currentlySelectedItem == "Pipebomb")
        {
            ChangeSlots(nothingEquipped);
        }

        //Gauze is slot 4
        if (Input.GetButtonDown("Slot4") && hasGauze == true && currentlySelectedItem != "Gauze")
        {
            ChangeSlots(gauze);
        }
        else if (Input.GetButtonDown("Slot4") && hasGauze == true && currentlySelectedItem == "Gauze")
        {
            ChangeSlots(nothingEquipped);
        }
    }

    //Changes currently equipped item to whatever is passed in.
    public void ChangeSlots(GameObject selectedSlot)
    {
        if (pistol.activeSelf == true && currentlySelectedItem == "Pistol")
        {
            CancelReload();
        }
        if (gauze.activeSelf == true && currentlySelectedItem == "Gauze" && UseConsumableItem.playerIsHealing)
        {
            UCI.StopHeal();
        }

        nothingEquipped.SetActive(false);
        machete.SetActive(false);
        pistol.SetActive(false);
        pipebomb.SetActive(false);
        gauze.SetActive(false);

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
    }
}
