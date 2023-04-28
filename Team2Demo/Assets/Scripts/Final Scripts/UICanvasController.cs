using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICanvasController : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject loseMenu;
    [SerializeField] public GameObject winMenu;

    [SerializeField] Button pauseMenuResumeButton;
    [SerializeField] Button pauseMenuRestartButton;
    [SerializeField] Button pauseMenuMainMenuButton;

    [SerializeField] Button reloadLevelOneButton;
    [SerializeField] Button reloadLevelTwoButton;
    [SerializeField] Button reloadLevelThreeButton;
    [SerializeField] Button reloadLevelBossButton;
    [SerializeField] Button loseMenuMainMenuButton;

    [SerializeField] Button winMenuHubButton;
    [SerializeField] Button winMenuMainMenuButton;

    void Awake()
    {
        pauseMenuResumeButton.onClick.AddListener(ResumeGame);
        pauseMenuRestartButton.onClick.AddListener(GoToHub);
        pauseMenuMainMenuButton.onClick.AddListener(GoToMainMenu);

        reloadLevelOneButton.onClick.AddListener(LoadLevelOne);
        reloadLevelTwoButton.onClick.AddListener(LoadLevelTwo);
        reloadLevelThreeButton.onClick.AddListener(LoadLevelThree);
        reloadLevelBossButton.onClick.AddListener(LoadLevelBoss);
        loseMenuMainMenuButton.onClick.AddListener(GoToMainMenu);

        winMenuHubButton.onClick.AddListener(GoToHub);
        winMenuMainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void Start()
    {
        // pauseMenu.SetActive(false);
        // loseMenu.SetActive(false);
        // winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        loseMenu.SetActive(false);
        winMenu.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void GoToMainMenu()
    {
        ScenesManagerSingleton.Instance.LoadMainMenuScene();
        Time.timeScale = 1f;
    }

    void GoToHub()
    {
        ScenesManagerSingleton.Instance.LoadHubScene();
        Time.timeScale = 1f;
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void LoadLevelOne()
    {
        //ScenesManagerSingleton.Instance.LoadLevelOne();
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

    void LoadLevelTwo()
    {
        //ScenesManagerSingleton.Instance.LoadLevelOne();
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }

    void LoadLevelThree()
    {
        //ScenesManagerSingleton.Instance.LoadLevelOne();
        SceneManager.LoadScene(4);
        Time.timeScale = 1f;
    }

    void LoadLevelBoss()
    {
        //ScenesManagerSingleton.Instance.LoadLevelOne();
        SceneManager.LoadScene(5);
        Time.timeScale = 1f;
    }

}
