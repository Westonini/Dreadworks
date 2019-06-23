using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarryOverInventory : MonoBehaviour
{
    public static CarryOverInventory instance;

    [HideInInspector]
    public bool carryOverHasMachete, carryOverHasPistol, carryOverHasPipebomb, carryOverHasGauze;

    [HideInInspector]
    public int carryOverPistolParts, carryOverMacheteParts, carryOverAmmo, carryOverBulletCasings, carryOverGunpowder, carryOverFuses, carryOverCloth;

    [HideInInspector]
    public int carryOverPipebombCount = 0, carryOverGauzeCount = 0;

    [HideInInspector]
    public static bool levelStart;

    SlotSelection SS;
    Inventory inv;

    void Awake()
    {
        //DontDestroyOnLoad
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        //Get the SlotSelection and Inventory scripts with the start of each scene.
        if (levelStart)
        {
            SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
            inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

            levelStart = false;       
        }

        //If the current scene is the Main Menu destroy this object.
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "MainMenu")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Saved all the information on how much materials the player has and if they have certain items.
        if (other.gameObject.tag == "Player")
        {
            carryOverHasMachete = SS.hasMachete;
            carryOverHasPistol = SS.hasPistol;
            carryOverHasPipebomb = SS.hasPipebomb;
            carryOverHasGauze = SS.hasGauze;

            carryOverPistolParts = inv.pistolParts;
            carryOverMacheteParts = inv.macheteParts;
            carryOverAmmo = inv.ammo;
            carryOverBulletCasings = inv.bulletCasings;
            carryOverGunpowder = inv.gunpowder;
            carryOverFuses = inv.fuses;
            carryOverCloth = inv.cloth;

            carryOverPipebombCount = inv.pipebombCount;
            carryOverGauzeCount = inv.gauzeCount;

            //Change to next scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
