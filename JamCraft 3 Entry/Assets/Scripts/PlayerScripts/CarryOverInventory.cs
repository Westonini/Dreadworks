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
        //If the current scene is the Main Menu destroy this object.
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "MainMenu")
        {
            Destroy(gameObject);
        }
    }
}
