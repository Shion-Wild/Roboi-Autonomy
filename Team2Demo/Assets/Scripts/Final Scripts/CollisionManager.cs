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
                SceneManager.LoadScene(2);
                break;
            case "GoToTwo":
                SceneManager.LoadScene(3);
                break;
            case "GoThree":
                SceneManager.LoadScene(4);
                break;
            case "GoToWin":
                SceneManager.LoadScene(6);
                break;
            case "GoToLose":
                SceneManager.LoadScene(7);
                break;
            case "GoToBoss":
                SceneManager.LoadScene(5);
                break;
            case "DeathPlatform":
                SceneManager.LoadScene(7);
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
            default:
                break;
        }
        
    }
}
