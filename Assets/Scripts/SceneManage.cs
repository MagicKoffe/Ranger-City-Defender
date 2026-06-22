using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField] string mainMenuScene;
    [SerializeField] string settingsScene;
    [SerializeField] string gameScene;

    private static SceneManage _instance;

    public static SceneManage Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void loadSettingScene()
    {
        SceneManager.LoadScene(settingsScene, LoadSceneMode.Additive);
    }

    public void closeSettings()
    {
        SceneManager.UnloadSceneAsync(settingsScene);
    }
}
