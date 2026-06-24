using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;

    public delegate void PauseGame(bool paused);
    public static event PauseGame togglePauseEvent;

    public void ToggleSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void TogglePause()
    {
        //Send out event to know wether game has been paused or unpaused

        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        togglePauseEvent(!pauseMenu.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        settingsMenu.SetActive(false);
    }


    //INPUT ----------------------------------------------
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pause");
            TogglePause();
        }
    }
}
