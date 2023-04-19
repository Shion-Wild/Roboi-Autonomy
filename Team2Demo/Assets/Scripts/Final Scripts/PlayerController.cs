using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Static Variables
    static int maxHealth = 8;
    static int currentHealth;

    public HealthBar healthBar;
    //public GameObject respawnPoint;

    // Music and SFX Clips
    [SerializeField] public AudioClip playerDeath;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //respawnPoint = GameObject.FindGameObjectWithTag("SpawnTrigger");
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(7);
        }
        
    }

    void PlayerTakeDamage()
    {
        currentHealth --;
        healthBar.SetHealth(currentHealth);
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerTakeDamage(1);
        }
    }
    */

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("EnemyProjectile"))
        {
            PlayerTakeDamage();
        }
        if (other.CompareTag("DeathPlatform"))
        {
            //playerScript.PlayDeath();
            SceneManager.LoadScene(6);
            SoundManager.Instance.PlayBackgroundMusic(playerDeath);
            //transform.position = respawnPoint.transform.position;
        }

    }


}
