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
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void loadGame()
    {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }

    public void loadSettingScene()
    {
        SceneManager.LoadScene(settingsScene, LoadSceneMode.Additive);
    }

    public void closeSettings()
    {
        SceneManager.UnloadSceneAsync(settingsScene);
    }

    public void exitGame()
    {
        //exit game
    }
}
