using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagerSingleton : MonoBehaviour
{
    private static ScenesManagerSingleton instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static ScenesManagerSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                // Try to find an existing instance
                instance = FindObjectOfType<ScenesManagerSingleton>();

                // If not found, create a new instance
                if (instance == null)
                {
                    GameObject obj = new GameObject("ScenesManagerSingleton");
                    instance = obj.AddComponent<ScenesManagerSingleton>();
                }
            }

            return instance;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinMenu");
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseMenu");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}