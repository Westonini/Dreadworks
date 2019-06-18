using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public static int health = 100;
    public TextMeshProUGUI healthWord;
    public TextMeshProUGUI healthText;

    void Update()
    {
        //Player death
        if (health <= 0)
        {
            health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    }
}
