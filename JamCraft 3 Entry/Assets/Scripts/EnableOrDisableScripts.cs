using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOrDisableScripts : MonoBehaviour
{
    private PlayerController PC;
    private PlayerShooting PS;
    private PlayerMelee PM;
    private UseConsumableItem UCI;
    private SlotSelection SS;

    public void EnableDisableScripts(bool TF) //true to enable, false to disable.
    {
        if (PC != null)
        {
            PC.enabled = TF;
        }
        if (PS != null)
        {
            PS.enabled = TF;
        }
        if (PM != null)
        {
            PM.enabled = TF;
        }
        if (UCI != null)
        {
            UCI.enabled = TF;
        }
        if (SS != null)
        {
            SS.enabled = TF;
        }
    }

    public void GetScripts() //get script components
    {
        try
        {
            PC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
        catch
        {
            PC = null;
        }

        try
        {
            PS = GameObject.Find("Pistol").GetComponent<PlayerShooting>();
        }
        catch
        {
            PM = null;
        }

        try
        {
            PM = GameObject.Find("Machete").GetComponent<PlayerMelee>();
        }
        catch
        {
            PM = null;
        }

        try
        {
            UCI = GameObject.Find("Player").GetComponent<UseConsumableItem>();
            if (UCI != null)
            {
                UCI.StopHeal();
            }
        }
        catch
        {
            UCI = null;
        }

        try
        {
            SS = GameObject.FindWithTag("Player").GetComponent<SlotSelection>();
            if (PS != null)
            {
                SS.CancelReload();
            }
        }
        catch
        {
            SS = null;
        }
    }
}
