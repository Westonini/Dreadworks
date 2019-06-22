using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    EnemyPatrol EP;

    public AudioSource footsteps;
    [HideInInspector]
    public bool playingFootstepsSound = false;

    void Awake()
    {
        try
        {
            EP = gameObject.GetComponent<EnemyPatrol>();
        }
        catch
        {
            EP = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EP != null)
        {
            //Footsteps sounds during patrol
            if (EP.startWait)
            {
                footsteps.Pause();
                playingFootstepsSound = false;
            }
            else if (!EP.startWait && !playingFootstepsSound)
            {
                footsteps.Play();
                playingFootstepsSound = true;
            }
        }
        else
        {
            footsteps.Play();
            playingFootstepsSound = true;
        }
    }
}
