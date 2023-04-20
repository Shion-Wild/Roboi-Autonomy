using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMPCooldown : MonoBehaviour
{
    public float maxCooldownTime = 3.0f; // The maximum cooldown time for the ability
    public Image cooldownImage; // Reference to the ability icon image
    private float currentCooldownTime = 0.0f; // The current cooldown time
    private bool isOnCooldown = false; // Whether the ability is currently on cooldown

    void Update()
    {
        if (isOnCooldown)
        {
            currentCooldownTime -= Time.deltaTime; // Update the cooldown timer
            if (currentCooldownTime <= 0.0f)
            {
                isOnCooldown = false; // Cooldown is over
                cooldownImage.fillAmount = 0.0f; // Reset the ability icon image fill
            }
            else
            {
                cooldownImage.fillAmount = currentCooldownTime / maxCooldownTime; // Update the ability icon image fill
            }
        }
    }

    public void TriggerAbility()
    {
        if (!isOnCooldown)
        {
            // Trigger the ability's effect
            Debug.Log("Ability triggered!");
            isOnCooldown = true; // Set the ability on cooldown
            currentCooldownTime = maxCooldownTime; // Set the current cooldown time to the maximum cooldown time
        }
    }
}
