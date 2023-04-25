using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    // Cached References 
    PlayerController playerController;

    // Audio Clips
    [SerializeField] public AudioClip playerDeath;

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
                ScenesManagerSingleton.Instance.LoadScene("Level 1");
                break;
            case "GoToTwo":
                //SceneManager.LoadScene(3);
                ScenesManagerSingleton.Instance.LoadScene("Level 2");
                break;
            case "GoToThree":
                //SceneManager.LoadScene(4);
                ScenesManagerSingleton.Instance.LoadScene("Level 3");
                break;
            case "GoToWin":
                //SceneManager.LoadScene(6);
                ScenesManagerSingleton.Instance.LoadWinScene();
                break;
            case "GoToLose":
                //SceneManager.LoadScene(7);
                ScenesManagerSingleton.Instance.LoadLoseScene();
                break;
            case "GoToBoss":
                //SceneManager.LoadScene(5);
                ScenesManagerSingleton.Instance.LoadScene("Boss Level");
                break;
            case "GoToHub":
                SceneManager.LoadScene(1);
                //ScenesManagerSingleton.Instance.LoadScene("Level 1");
                break;
            case "DeathPlatform":
                //SceneManager.LoadScene(7);
                ScenesManagerSingleton.Instance.LoadLoseScene();
                SoundManager.Instance.PlayLossMusic();
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
}
