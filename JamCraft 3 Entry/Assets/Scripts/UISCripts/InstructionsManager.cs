using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionsManager : MonoBehaviour
{
    public bool newTextAdded = false;

    public TextMeshProUGUI instructionsText;

    // Update is called once per frame
    void Update()
    {
        if (newTextAdded)
        {
            CancelInvoke("RemoveInstructionsText");
            Invoke("RemoveInstructionsText", 10);
            newTextAdded = false;
        }
    }

    public void RemoveInstructionsText()
    {
        instructionsText.text = "";
    }
}
