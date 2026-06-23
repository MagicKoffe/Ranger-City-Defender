using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button openSettingButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openSettingButton.onClick.AddListener(SceneManage.Instance.loadSettingScene);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
