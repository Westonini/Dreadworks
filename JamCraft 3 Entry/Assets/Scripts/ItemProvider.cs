using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProvider : MonoBehaviour
{
    public bool willContainPistolParts = false;
    public bool willContainMacheteParts = false;
    public bool willContainAmmo = false;
    public int ammoToBeGiven;

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
    }
}
