using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSounds : MonoBehaviour
{
    public AudioSource[] sounds;
    private float soundCooldown;
    private bool soundIsOnCooldown = true;
    private bool soundCooldownTimeChosen = false;
    private bool soundToPlaySelected = false;
    private bool soundCurrentlyPlaying = false;
    private int soundToPlay;

    // Update is called once per frame
    void Update()
    {
        if (!soundIsOnCooldown)
        {
            if (!soundToPlaySelected)
            {
                soundToPlay = Random.Range(1, sounds.Length);
                soundToPlaySelected = true;                          
            }
            else if (soundToPlaySelected && !soundCurrentlyPlaying)
            {
                sounds[soundToPlay].Play();
                Invoke("soundFinishedPlaying", 6);
                soundCurrentlyPlaying = true;
            }

            soundIsOnCooldown = true;
        }

        if (soundIsOnCooldown)
        {
            if (!soundCooldownTimeChosen)
            {
                soundCooldown = Random.Range(6, 12);
                soundCooldownTimeChosen = true;
            }
            else
            {
                soundCooldown -= Time.deltaTime;

                if (soundCooldown <= 0)
                {
                    soundCooldown = 0;
                    soundIsOnCooldown = false;
                }
            }
        }
    }

    void soundFinishedPlaying()
    {
        soundIsOnCooldown = true;
        soundCurrentlyPlaying = false;
        soundToPlaySelected = false;
        soundCooldownTimeChosen = false;
    }
}
