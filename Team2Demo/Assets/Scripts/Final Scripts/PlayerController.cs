using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;

    //public HealthBar healthBar;
    
    void Start()
    {
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }

    void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthBar.SetHealth(currentHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            PlayerTakeDamage(1);
        }
    }

}
