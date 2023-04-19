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
    
    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(6);
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
        
    }

}
