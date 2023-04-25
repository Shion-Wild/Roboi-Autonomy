using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    PlayerController playerController;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        
    }
    public void PlayGame ()
    {
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        // Load the current scene of the player 
        SceneManager.LoadScene(1);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void Controls ()
    {
        SceneManager.LoadScene(8);
    }

    public void credits()
    {
        SceneManager.LoadScene(9);
    }
}
