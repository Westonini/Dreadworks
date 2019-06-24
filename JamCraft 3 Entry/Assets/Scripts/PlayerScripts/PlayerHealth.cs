using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [HideInInspector]
    public static int health;
    public TextMeshProUGUI healthWord;
    public TextMeshProUGUI healthText;
    public GameObject injuredPanel;
    public GameObject playerRagdoll;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        //Player death
        if (health <= 0)
        {
            health = 0;

            GameObject ragdollInstance;
            ragdollInstance = Instantiate(playerRagdoll, transform.position, transform.rotation) as GameObject;

            Destroy(gameObject);
        }
        if (health > 100)
        {
            health = 100;
        }

        //HealthText update
        healthWord.text = "Health:";
        healthText.text = health.ToString() + " / 100";

        //HealthText color
        if (health >= 70)
        {         
            healthText.color = new Color32(0, 255, 0, 50);
        }
        else if (health < 70 && health >= 35)
        {
            healthText.color = new Color32(255, 255, 0, 50);
        }
        else
        {
            healthText.color = new Color32(255, 0, 0, 50);
        }

        if (health < 35 && !injuredPanel.activeSelf)
        {
            injuredPanel.SetActive(true);

            //SoundEffect
            FindObjectOfType<AudioManager>().Play("HeavyBreathing");
            FindObjectOfType<AudioManager>().Play("Heartbeat");
        }
        else if (health >= 35)
        {
            injuredPanel.SetActive(false);

            //SoundEffect
            FindObjectOfType<AudioManager>().Stop("HeavyBreathing");
            FindObjectOfType<AudioManager>().Stop("Heartbeat");
        }
    }
}
