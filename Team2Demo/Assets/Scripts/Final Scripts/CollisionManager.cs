using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject sceneTransitionOne;
    [SerializeField] GameObject sceneTransitionTwo;
    [SerializeField] GameObject sceneTransitionThree;
    [SerializeField] GameObject sceneTransitionBoss;


    // Cached References 
    PlayerController playerController;

    // Audio Clips
    [SerializeField] public AudioClip playerDeath;


    float waitTime = 3f;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "GoToOne":
                //SceneManager.LoadScene(2);
                StartCoroutine(TransitionOne());
                //ScenesManagerSingleton.Instance.LoadLevelOne();
                break;
            case "GoToTwo":
                //SceneManager.LoadScene(3);
                StartCoroutine(TransitionTwo());
                //ScenesManagerSingleton.Instance.LoadLevelTwo();
                break;
            case "GoToThree":
                //SceneManager.LoadScene(4);
                StartCoroutine(TransitionThree());
                //ScenesManagerSingleton.Instance.LoadLevelThree();
                break;
            case "GoToWin":
                //SceneManager.LoadScene(6);
                //ScenesManagerSingleton.Instance.LoadWinScene();
                break;
            case "GoToLose":
                //SceneManager.LoadScene(7);
                //ScenesManagerSingleton.Instance.LoadLoseScene();
                break;
            case "GoToBoss":
                //SceneManager.LoadScene(5);
                 StartCoroutine(TransitionBoss());
                //ScenesManagerSingleton.Instance.LoadLevelBoss();
                break;
            case "GoToHub":
                SceneManager.LoadScene(2);
                //ScenesManagerSingleton.Instance.LoadScene("Level 1");
                break;
            case "DeathPlatform":
                loseMenu.SetActive(true);
                SoundManager.Instance.PlayLossMusic();
                Time.timeScale = 0f;
                break;
            case "ActivateEMP":
                MovementController.isEMPActivated = true;
                Destroy(other.gameObject);
                break;
            case "ActivateInvisible":
                MovementController.isInvisibilityActivated = true;
                Destroy(other.gameObject);
                break;
            case "EnemyProjectile":
                playerController.PlayerTakeDamage();
                break;
            case "HealthPickup":
                playerController.HealthPickup();
                Destroy(other.gameObject);
                break;
            default:
                break;
        }
        
    }

    private IEnumerator TransitionOne()
    {
        // Display the loading image for a few seconds
        sceneTransitionOne.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        ScenesManagerSingleton.Instance.LoadLevelOne();
    }

    private IEnumerator TransitionTwo()
    {
        // Display the loading image for a few seconds
        sceneTransitionTwo.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        ScenesManagerSingleton.Instance.LoadLevelTwo();
    }

    private IEnumerator TransitionThree()
    {
        // Display the loading image for a few seconds
        sceneTransitionThree.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        ScenesManagerSingleton.Instance.LoadLevelThree();
    }

    private IEnumerator TransitionBoss()
    {
        // Display the loading image for a few seconds
        sceneTransitionBoss.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        ScenesManagerSingleton.Instance.LoadLevelBoss();
    }


}
