using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject winMenu;

    [SerializeField] Button mainMenu;
    //[SerializeField] Button restart;

    void Awake()
    {
        pauseMenu.SetActive(true);
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
    }

    void Start()
    {
        mainMenu.onClick.AddListener(GoToMainMenu);
    }

    void GoToMainMenu()
    {
        ScenesManagerSingleton.Instance.LoadMainMenuScene();
    }

 

}
