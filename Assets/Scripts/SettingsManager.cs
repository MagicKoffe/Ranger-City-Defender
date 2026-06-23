using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Button exitSettingButton;

    void Start()
    {
        exitSettingButton.onClick.AddListener(SceneManage.Instance.closeSettings);
    }

}
