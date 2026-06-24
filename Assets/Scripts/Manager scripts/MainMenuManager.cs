using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingsPage;

    public void openSettingsPage()
    {
        Debug.Log("Open settings page");
        settingsPage.SetActive(true);
    }
}
