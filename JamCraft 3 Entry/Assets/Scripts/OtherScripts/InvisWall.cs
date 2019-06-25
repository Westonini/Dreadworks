using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvisWall : MonoBehaviour
{
    private Collider invisCollider;
    public TextMeshProUGUI goalsText;

    private bool textCleared = false;

    [Space]
    public bool collectMachetePartsGoal = false;
    public bool craftMacheteGoal = false;
    public bool craftPistolGoal = false;

    Inventory inv;
    SlotSelection SS;

    private void Awake()
    {
        invisCollider = GetComponent<Collider>();
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        SS = GameObject.FindGameObjectWithTag("Player").GetComponent<SlotSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collectMachetePartsGoal && inv.macheteParts == 3 && !textCleared)
        {
            invisCollider.enabled = false;
            goalsText.text = "";
            textCleared = true;
        }
        if (craftMacheteGoal && SS.hasMachete && !textCleared)
        {
            invisCollider.enabled = false;
            goalsText.text = "";
            textCleared = true;
        }
        if (craftPistolGoal && SS.hasPistol)
        {
            invisCollider.enabled = false;
            goalsText.text = "";
            textCleared = true;
        }
    }
}
