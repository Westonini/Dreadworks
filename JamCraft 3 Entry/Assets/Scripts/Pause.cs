using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    private bool gameNotInFocus = false;
    private bool flashlightWasOn = false;
    public GameObject pauseMenu;
    SearchObject SO;
    ToggleFlashlight TF;

    void Awake()
    {
        SO = GameObject.FindGameObjectWithTag("Interact").GetComponent<SearchObject>();
        TF = GameObject.FindGameObjectWithTag("Player").GetComponent<ToggleFlashlight>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") || gameNotInFocus)
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }           
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        gameNotInFocus = !hasFocus;
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        SO.GetScripts();
        SO.EnableDisableScripts(false);
        flashlightWasOn = TF.flashlightIsOn;
        TF.ToggleFlashLight(false);
        TF.enabled = false;
        Time.timeScale = 0;
        isPaused = true;
    }

    private void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        SO.EnableDisableScripts(true);
        TF.enabled = true;
        if (flashlightWasOn)
        {
            TF.ToggleFlashLight(true);
        }
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
       SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
