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
    public bool craftKeyGoal = false;

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
            ClearGoalText();
        }
        if (craftMacheteGoal && SS.hasMachete && !textCleared)
        {
            ClearGoalText();
        }
        if (craftPistolGoal && SS.hasPistol && !textCleared)
        {
            ClearGoalText();
        }
        if (craftKeyGoal && SS.hasKey && !textCleared)
        {
            invisCollider.enabled = false;
            goalsText.text = "- Use Key to unlock the locked door";
            textCleared = true;
        }
    }

    void ClearGoalText()
    {
        invisCollider.enabled = false;
        goalsText.text = "";
        textCleared = true;
    }
}
