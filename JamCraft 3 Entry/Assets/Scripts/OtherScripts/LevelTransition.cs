using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public bool goToNextLevel = true;
    public bool goToMainMenu = false;

    CarryOverInventory COI;
    SlotSelection SS;
    Inventory inv;
    Pause pause;

    // Start is called before the first frame update
    void Awake()
    {
        COI = GameObject.FindGameObjectWithTag("CarryOverInventory").GetComponent<CarryOverInventory>();
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        pause = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Pause>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Saved all the information on how much materials the player has and if they have certain items.
        if (other.gameObject.tag == "Player")
        {
            COI.carryOverHasMachete = SS.hasMachete;
            COI.carryOverHasPistol = SS.hasPistol;
            COI.carryOverHasPipebomb = SS.hasPipebomb;
            COI.carryOverHasGauze = SS.hasGauze;

            COI.carryOverPistolParts = inv.pistolParts;
            COI.carryOverMacheteParts = inv.macheteParts;
            COI.carryOverAmmo = inv.ammo;
            COI.carryOverBulletCasings = inv.bulletCasings;
            COI.carryOverGunpowder = inv.gunpowder;
            COI.carryOverFuses = inv.fuses;
            COI.carryOverCloth = inv.cloth;

            COI.carryOverPipebombCount = inv.pipebombCount;
            COI.carryOverGauzeCount = inv.gauzeCount;

            if (goToNextLevel)
            {
                //Change to next scene.
                pause.UnPauseGame();
                FindObjectOfType<AudioManager>().Stop("Walking");
                FindObjectOfType<AudioManager>().Stop("Sneaking");
                FindObjectOfType<AudioManager>().Stop("BombSizzle");
                FindObjectOfType<AudioManager>().Stop("Heartbeat");
                FindObjectOfType<AudioManager>().Stop("HeavyBreathing");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (goToMainMenu)
            {
                //Change to main menu.
                pause.UnPauseGame();
                FindObjectOfType<AudioManager>().Stop("Walking");
                FindObjectOfType<AudioManager>().Stop("Sneaking");
                FindObjectOfType<AudioManager>().Stop("BombSizzle");
                FindObjectOfType<AudioManager>().Stop("Heartbeat");
                FindObjectOfType<AudioManager>().Stop("HeavyBreathing");
                SceneManager.LoadScene(0);
            }

            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
