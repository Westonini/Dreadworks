using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProvider : MonoBehaviour
{
    public bool willContainPistolParts = false;
    public bool willContainMacheteParts = false;
    public bool willContainAmmo = false;
    public bool willContainBulletCasings = false;
    public bool willContainGunpowder = false;
    public bool willContainPipebomb = false;
    public bool willContainFuses = false;

    [Space]
    public bool randomizeDrops = false;

    [HideInInspector]
    public bool checkedThisProvider = false;

    [HideInInspector]
    public List<string> itemsToGive = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        if (willContainPistolParts == true)
        {
            itemsToGive.Add("PistolPart");
        }
        if (willContainMacheteParts == true)
        {
            itemsToGive.Add("MachetePart");
        }
        if (willContainAmmo == true)
        {
            itemsToGive.Add("Ammo");
        }
        if (willContainBulletCasings == true)
        {
            itemsToGive.Add("BulletCasings");
        }
        if (willContainGunpowder == true)
        {
            itemsToGive.Add("Gunpowder");
        }
        if (willContainPipebomb == true)
        {
            itemsToGive.Add("Pipebomb");
        }
        if (willContainFuses == true)
        {
            itemsToGive.Add("Fuses");
        }


        if (randomizeDrops == true)
        {
            int itemsToDrop = Random.Range(2, 3); //Can drop 2-3 items from randomizer

            for (int i = 1; i <= itemsToDrop; i++)
            {
                int itemToGive = Random.Range(1, 5);

                switch (itemToGive)
                {
                    case 1:
                        itemsToGive.Add("Ammo");
                        break;
                    case 2:
                        itemsToGive.Add("BulletCasings");
                        break;
                    case 3:
                        itemsToGive.Add("Gunpowder");
                        break;
                    case 4:
                        itemsToGive.Add("Fuses");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
