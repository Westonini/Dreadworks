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
    public bool willContainGauze = false;
    public bool willContainCloth = false;
    public bool willContainKeyFragments = false;

    [Space]
    public bool randomizeDrops = false;
    public bool cheatChest = false;

    [HideInInspector]
    public bool checkedThisProvider = false;

    [HideInInspector]
    public List<string> itemsToGive = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        if (willContainPistolParts)
        {
            itemsToGive.Add("PistolPart");
        }
        if (willContainMacheteParts)
        {
            itemsToGive.Add("MachetePart");
        }
        if (willContainAmmo)
        {
            itemsToGive.Add("Ammo");
        }
        if (willContainBulletCasings)
        {
            itemsToGive.Add("BulletCasings");
        }
        if (willContainGunpowder)
        {
            itemsToGive.Add("Gunpowder");
        }
        if (willContainPipebomb)
        {
            itemsToGive.Add("Pipebomb");
        }
        if (willContainFuses)
        {
            itemsToGive.Add("Fuses");
        }
        if (willContainGauze)
        {
            itemsToGive.Add("Gauze");
        }
        if (willContainCloth)
        {
            itemsToGive.Add("Cloth");
        }
        if (willContainKeyFragments)
        {
            itemsToGive.Add("KeyFragments");
        }

        if (cheatChest)
        {
            itemsToGive.Add("PistolPart");
            itemsToGive.Add("PistolPart");
            itemsToGive.Add("PistolPart");
            itemsToGive.Add("PistolPart");
            itemsToGive.Add("MachetePart");
            itemsToGive.Add("MachetePart");
            itemsToGive.Add("MachetePart");
            itemsToGive.Add("Ammo");
            itemsToGive.Add("Ammo");
            itemsToGive.Add("BulletCasings");
            itemsToGive.Add("Gunpowder");
            itemsToGive.Add("Pipebomb");
            itemsToGive.Add("Fuses");
            itemsToGive.Add("Gauze");
            itemsToGive.Add("Cloth");
            itemsToGive.Add("KeyFragments");
            itemsToGive.Add("KeyFragments");
            itemsToGive.Add("KeyFragments");
            itemsToGive.Add("KeyFragments");
        }

        if (randomizeDrops)
        {
            int itemsToDrop = Random.Range(2, 4); //Can drop 2-3 items from randomizer

            bool ammoAlreadyGiven = false;
            bool bulletCasingsAlreadyGiven = false;
            bool gunpowderAlreadyGiven = false;
            bool fusesAlreadyGiven = false;
            bool clothAlreadyGiven = false;

            for (int i = 1; i <= itemsToDrop; i++)
            {
                int itemToGive = Random.Range(1, 6);

                switch (itemToGive)
                {
                    //Ammo Case
                    case 1:
                        if (!ammoAlreadyGiven)
                        {
                            itemsToGive.Add("Ammo");
                            ammoAlreadyGiven = true;
                            break;
                        }
                        else
                        {
                            i -= 1;
                            continue;
                        }

                    //BulletCasings Case
                    case 2:
                        if (!bulletCasingsAlreadyGiven)
                        {
                            itemsToGive.Add("BulletCasings");
                            bulletCasingsAlreadyGiven = true;
                            break;
                        }
                        else
                        {
                            i -= 1;
                            continue;
                        }

                    //Gunpowder Case
                    case 3:
                        if (!gunpowderAlreadyGiven)
                        {
                            itemsToGive.Add("Gunpowder");
                            gunpowderAlreadyGiven = true;
                            break;
                        }
                        else
                        {
                            i -= 1;
                            continue;
                        }

                    //Fuses Case
                    case 4:
                        if (!fusesAlreadyGiven)
                        {
                            itemsToGive.Add("Fuses");
                            fusesAlreadyGiven = true;
                            break;
                        }
                        else
                        {
                            i -= 1;
                            continue;
                        }

                    //Cloth Case
                    case 5:
                        if (!clothAlreadyGiven)
                        {
                            itemsToGive.Add("Cloth");
                            clothAlreadyGiven = true;
                            break;
                        }
                        else
                        {
                            i -= 1;
                            continue;
                        }

                    //Default Case (If there's an error)
                    default:
                        break;
                }
            }
        }
    }
}
