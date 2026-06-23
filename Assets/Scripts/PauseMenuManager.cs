using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;

    public void ToggleSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void TogglePause()
    {
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0f;
        }

        pauseMenu.SetActive(!pauseMenu.activeSelf);
        settingsMenu.SetActive(false);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pause");
            TogglePause();
        }
    }
}
