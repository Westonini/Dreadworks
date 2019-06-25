using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowInstructions : MonoBehaviour
{
    private Collider _collider;

    public TextMeshProUGUI instructionsText;
    public string instructions;

    [Space]
    public bool isAlsoAnInvisWall = false;
    public bool turnOnInvisWall = false;
    public Collider invisWall;

    [Space]
    public TextMeshProUGUI goalsText;
    public string[] newGoal;

    InstructionsManager IM;

    void Awake()
    {
        IM = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<InstructionsManager>();
    }

    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Tell the InstructionsManager script that new text has been added (used for removing the text).
            IM.newTextAdded = true;

            //Changes instructionsText to whatever instruction was inputted in the field in the inspector.
            instructionsText.text = instructions;

            //For each new goal, add them to the goal list.
            for (int i = 0; i < newGoal.Length; i++)
            {
                goalsText.text += newGoal[i] + "\n";
            }

            //Used for when the player must complete a goal before proceeding.
            if (turnOnInvisWall && invisWall != null)
            {
                invisWall.enabled = true;
            }
        }
        else if (other.gameObject.tag == "Interact" && isAlsoAnInvisWall)
        {
            //Tell the InstructionsManager script that new text has been added (used for removing the text).
            IM.newTextAdded = true;

            //Changes instructionsText to whatever instruction was inputted in the field in the inspector.
            instructionsText.text = instructions;
        }
    }
}
