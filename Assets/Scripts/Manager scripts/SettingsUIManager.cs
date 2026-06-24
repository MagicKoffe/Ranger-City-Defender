using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject audioPage;
    [SerializeField] private GameObject controlsPage;
    [SerializeField] private GameObject graphicsPage;

    void Start()
    {
        //exitSettingButton.onClick.AddListener(SceneManage.Instance.closeSettings);
    }

    public void enterGraphicsPage()
    {
        graphicsPage.SetActive(true);
        controlsPage.SetActive(false);
        audioPage.SetActive(false);
    }

    public void enterAudioPage()
    {
        graphicsPage.SetActive(false);
        controlsPage.SetActive(false);
        audioPage.SetActive(true);
    }

    public void enterControlsPage()
    {
        graphicsPage.SetActive(false);
        controlsPage.SetActive(true);
        audioPage.SetActive(false);
    }

    public void exitSettingsPage()
    {
        settingsCanvas.SetActive(false);
    }

}
