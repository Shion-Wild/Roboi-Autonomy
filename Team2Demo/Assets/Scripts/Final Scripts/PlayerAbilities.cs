using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class PlayerAbilities : MonoBehaviour
{
    // Invisibility Variables and References
    [Header("Invisibility Variables and References")]
    Renderer playerRenderer;
    GameObject [] enemyScripts;
    //EnemyCameraAI cameraAI;
    public Material invisibleMaterial;
    public Material normalMaterial;

    public float invisibilityCoolDown;
    float invisibilityLastShot;

    // EMP & Gravity Variables and References
    [Header("EMP & Gravity Variables and References")]
    public float grenadeThrowForce = 5f;
    public GameObject empPrefab;

    public float empCoolDown;
    float empLastShot;
   
    // Dash References
    CharacterController characterController;
    MovementController movementController;
    Vector3 cameraRelativeMovement;
    public float dashSpeed;
    public float dashTime;

    // For Dash Cooldown Timer
    public float dashCoolDown;
    float dashLastShot;


    void Awake()
    {
        // Invisibility Cache
        playerRenderer = GameObject.Find("Character").GetComponent<MeshRenderer>();

        // Caching Array of Game Objects that are tagged "Enemy"
        enemyScripts = GameObject.FindGameObjectsWithTag("Enemy");
        

        //cameraAI = GameObject.Find("EnemyCamera").GetComponent<EnemyCameraAI>();

        // Dash 
        characterController = FindObjectOfType<CharacterController>();
        movementController = GetComponent<MovementController>();

    }

    // Dash MGMT
    public void TriggerDash()
    {
        if (Time.time - dashLastShot < dashCoolDown)
        {
            return;
        }

        dashLastShot = Time.time;

        StartCoroutine(Dash());
    }

    public IEnumerator Dash()
    {
        cameraRelativeMovement = movementController.cameraRelativeMovement;
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            characterController.Move(characterController.transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }


    // EMP MGMT
    public void ThrowEMPGrenade()
    {
        if (Time.time - empLastShot < empCoolDown)
        {
            return;
        }

        empLastShot = Time.time;

        GameObject grenade = Instantiate(empPrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * grenadeThrowForce);
    }


    // Invisibility MGMT
    public void TriggerInvisibility()
    {
        if (Time.time - invisibilityLastShot < invisibilityCoolDown)
        {
            return;
        }

        invisibilityLastShot = Time.time;

        StartCoroutine(InvisibilityTimer(6f));
    }

   
    public IEnumerator InvisibilityTimer(float duration)
    {

        playerRenderer.material = invisibleMaterial;
        TellEnemiesInvisible();
        //cameraAI.playerInvisible = true;    

        yield return new WaitForSeconds(duration);

        playerRenderer.material = normalMaterial;
        TellEnemiesVisible();
        //cameraAI.playerInvisible = false;
    }

    public void TellEnemiesInvisible()
    {
        for (int i = 0; i < enemyScripts.Length; i++) 
        {
            if (enemyScripts[i] != null)
            {
                EnemyAI enemyAI = enemyScripts[i].GetComponent<EnemyAI>();
                enemyAI.playerInvisible = true;
            }
            
        }
    }

    public void TellEnemiesVisible()
    {
        for (int i = 0; i < enemyScripts.Length; i++)
        {
            if (enemyScripts[i] != null)
            {
                EnemyAI enemyAI = enemyScripts[i].GetComponent<EnemyAI>();
                enemyAI.playerInvisible = false;
            }
            
        }
    }


}
