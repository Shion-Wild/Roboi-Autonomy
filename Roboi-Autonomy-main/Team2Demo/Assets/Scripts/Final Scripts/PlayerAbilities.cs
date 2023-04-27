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
    // Cached References 
    GameObject[] normalEnemyScripts;
    GameObject[] enemyArmScripts;
    GameObject[] enemyCameraScripts;
    Renderer playerRenderer;
    CharacterController characterController;
    MovementController movementController;
    Vector3 cameraRelativeMovement;

    [Header("Invisibility")]
    public float invisibilityCoolDown;
    public Material invisibleMaterial;
    public Material normalMaterial;
    float invisibilityLastShot;

    [Header("EMP")]
    public float empCoolDown;
    public float grenadeThrowForce = 5f;
    public GameObject empPrefab;
    float empLastShot;

    [Header("Dash")]
    public float dashCoolDown;
    public float dashSpeed;
    public float dashTime;
    float dashLastShot;


    void Awake()
    {
        // Invisibility Cache
        playerRenderer = GameObject.Find("Character").GetComponent<MeshRenderer>();

        // Caching Array of Game Objects that are tagged "Enemy"
        normalEnemyScripts = GameObject.FindGameObjectsWithTag("Enemy");
        enemyArmScripts = GameObject.FindGameObjectsWithTag("EnemyArm");
        enemyCameraScripts = GameObject.FindGameObjectsWithTag("EnemyCamera");

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

        StartCoroutine(InvisibilityTimer(3f));
    }

   
    public IEnumerator InvisibilityTimer(float duration)
    {

        playerRenderer.material = invisibleMaterial;
        TellEnemiesInvisible();
        TellEnemiesArmsInvisible();
        TellEnemiesCamerasInvisible();    

        yield return new WaitForSeconds(duration);

        playerRenderer.material = normalMaterial;
        TellEnemiesVisible();
        TellEnemiesArmsVisible();
        TellEnemiesCamerasVisible();
    }

    // Looping through normal Enemy Scripts
    public void TellEnemiesInvisible()
    {
        for (int i = 0; i < normalEnemyScripts.Length; i++) 
        {
            if (normalEnemyScripts[i] != null)
            {
                EnemyAI enemyAI = normalEnemyScripts[i].GetComponent<EnemyAI>();
                enemyAI.playerInvisible = true;
            }
        }
    }
    public void TellEnemiesVisible()
    {
        for (int i = 0; i < normalEnemyScripts.Length; i++)
        {
            if (normalEnemyScripts[i] != null)
            {
                EnemyAI enemyAI = normalEnemyScripts[i].GetComponent<EnemyAI>();
                enemyAI.playerInvisible = false;
            }
        }
    }

    // Looping through Enemy Arm Scripts
    public void TellEnemiesArmsInvisible()
    {
        for (int i = 0; i < enemyArmScripts.Length; i++)
        {
            if (enemyArmScripts[i] != null)
            {
                EnemyArmAI enemyAI = enemyArmScripts[i].GetComponent<EnemyArmAI>();
                enemyAI.playerInvisible = true;
            }
        }
    }
    public void TellEnemiesArmsVisible()
    {
        for (int i = 0; i < enemyArmScripts.Length; i++)
        {
            if (enemyArmScripts[i] != null)
            {
                EnemyArmAI enemyAI = enemyArmScripts[i].GetComponent<EnemyArmAI>();
                enemyAI.playerInvisible = false;
            }
        }
    }

    // Looping through Enemy Camera Scripts
    public void TellEnemiesCamerasInvisible()
    {
        for (int i = 0; i < enemyCameraScripts.Length; i++)
        {
            if (enemyCameraScripts[i] != null)
            {
                EnemyCameraAI enemyAI = enemyCameraScripts[i].GetComponent<EnemyCameraAI>();
                enemyAI.playerInvisible = true;
            }
        }
    }
    public void TellEnemiesCamerasVisible()
    {
        for (int i = 0; i < enemyCameraScripts.Length; i++)
        {
            if (enemyCameraScripts[i] != null)
            {
                EnemyCameraAI enemyAI = enemyCameraScripts[i].GetComponent<EnemyCameraAI>();
                enemyAI.playerInvisible = false;
            }
        }
    }


}
