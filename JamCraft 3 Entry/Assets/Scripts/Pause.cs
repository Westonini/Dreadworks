using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    private bool gameNotInFocus = false;
    private bool flashlightWasOn = false;
    private bool workbenchWasOn = false;

    public GameObject pauseMenu;
    public GameObject workBenchMenu;

    EnableOrDisableScripts EODS;
    ToggleFlashlight TF;
    SearchObject SO;

    void Awake()
    {
        TF = GameObject.FindGameObjectWithTag("Player").GetComponent<ToggleFlashlight>();
        EODS = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnableOrDisableScripts>();
        SO = GameObject.FindGameObjectWithTag("Interact").GetComponent<SearchObject>();
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

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        EODS.GetScripts();
        EODS.EnableDisableScripts(false);
        flashlightWasOn = TF.flashlightIsOn;
        TF.ToggleFlashLight(false);
        TF.enabled = false;
        Time.timeScale = 0;
        isPaused = true;

        if (workBenchMenu.activeSelf)
        {
            workbenchWasOn = true;
            workBenchMenu.SetActive(false);
        }
    }

    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);       
        TF.enabled = true;
        if (flashlightWasOn)
        {
            TF.ToggleFlashLight(true);
        }
        if (workbenchWasOn)
        {
            workBenchMenu.SetActive(true);
            workbenchWasOn = false;
        }
        if (SO.searching)
        {
            EODS.EnableDisableScripts(false);
        }
        else
        {
            EODS.EnableDisableScripts(true);
        }
        Time.timeScale = 1;
        isPaused = false;
    }

    public void ResumeGame()
    {
        UnPauseGame();
    }
    public void GoToMainMenu()
    {
        UnPauseGame();
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        UnPauseGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
