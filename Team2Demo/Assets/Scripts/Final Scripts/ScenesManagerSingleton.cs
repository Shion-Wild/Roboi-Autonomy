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

    public void LoadHubScene()
    {
        SceneManager.LoadScene("HubLevel");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevelBoss()
    {
        SceneManager.LoadScene(5);
    }

    // public void LoadWinScene()
    // {
    //     SceneManager.LoadScene("WinMenu");
    // }

    // public void LoadLoseScene()
    // {
    //     SceneManager.LoadScene("LoseMenu");
    // }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}