using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseSceneTrigger : MonoBehaviour
{
    PlayerMovementUpdated playerScript;

    /*
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }


    void ReloadLevel()
    {
        // Can Pass Index or String
        SceneManager.LoadScene(0);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    */

    void Start()
    {
        playerScript = GameObject.Find("Updated 3rd Person Player").GetComponent<PlayerMovementUpdated>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.PlayDeath();
            SceneManager.LoadScene(4);
        }
    }

}