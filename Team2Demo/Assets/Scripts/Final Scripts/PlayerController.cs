using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Cached References
    public HealthBar healthBar;

    // Static Health Variables
    static int maxHealth = 16;
    static int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        if (currentHealth <= 0)
        {
            //SceneManager.LoadScene(7);
        }
    }

    public void PlayerTakeDamage()
    {
        currentHealth = currentHealth -1;
        healthBar.SetHealth(currentHealth);
    }

    public void HealthPickup()
    {
        currentHealth = currentHealth +8;
        healthBar.SetHealth(currentHealth);
    }



}
