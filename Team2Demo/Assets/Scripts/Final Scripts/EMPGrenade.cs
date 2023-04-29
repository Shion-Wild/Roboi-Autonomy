using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EMPGrenade : MonoBehaviour
{

    float delay = 2f;
    float force = 100f;
    float radius = 20f;

    // Layer Mask for EMP Testing
    int layerMask = 1 << 10;

    // Particle FX
    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);


        foreach (Collider nearbyObject in colliders)
        {
            // Check if the nearby object has a specific tag
            if (nearbyObject.CompareTag("Enemy"))
            {
                // Get the components of the nearby object
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                EnemyAI enemyScript = nearbyObject.GetComponent<EnemyAI>();

                // If the nearby object has a Rigidbody component
                if (rb != null)
                {
                    // Apply explosion force to the Rigidbody
                    rb.AddExplosionForce(force, transform.position, radius);
                }
                if (enemyScript != null)
                {
                    // Call a method to disable the enemy's AI
                    enemyScript.DisableEnemy();
                }

            }
            else if (nearbyObject.CompareTag("EMPDoor"))
            {

                // Get the components of the nearby object
                DestroyableObject destroyableObject = nearbyObject.GetComponent<DestroyableObject>();
                Collider collider = nearbyObject.GetComponent<Collider>();

                if (destroyableObject != null)
                {
                    // Call a method to destroy the object as part of an EMP effect
                    destroyableObject.EMPDestroyDoor();
                }

                if (collider != null)
                {
                    // Destroy the collider component
                    Destroy(collider);
                }
            }
            else if (nearbyObject.CompareTag("Level3Exit"))
            {
                // Get the components of the nearby object
                EmpLevel3Detect level3Detect = nearbyObject.GetComponent<EmpLevel3Detect>();

                if (level3Detect != null)
                {
                    // Call a method to destroy the object as part of an EMP effect
                    level3Detect.EMPOpenLevel3();
                }


            }
            else if (nearbyObject.CompareTag("BossHitOne"))
            {
                // Get the components of the nearby object
                BossDestroyableObjectOne bossDestroyableObject = nearbyObject.GetComponent<BossDestroyableObjectOne>();

                // If the nearby object has a BossDestroyableObject component
                if (bossDestroyableObject != null)
                {
                    // Call a method to destroy the object as part of an EMP effect
                    bossDestroyableObject.ChangeLightsDamage();
                }


            }
            else if (nearbyObject.CompareTag("BossHitTwo"))
            {
                // Get the components of the nearby object
                BossDestroyableObjectTwo bossDestroyableObject = nearbyObject.GetComponent<BossDestroyableObjectTwo>();

                // If the nearby object has a BossDestroyableObject component
                if (bossDestroyableObject != null)
                {
                    // Call a method to destroy the object as part of an EMP effect
                    bossDestroyableObject.ChangeLightsDamage();
                }


            }
            else if (nearbyObject.CompareTag("BossHitThree"))
            {
                // Get the components of the nearby object
                BossDestroyableObjectThree bossDestroyableObject = nearbyObject.GetComponent<BossDestroyableObjectThree>();

                // If the nearby object has a BossDestroyableObject component
                if (bossDestroyableObject != null)
                {
                    // Call a method to destroy the object as part of an EMP effect
                    bossDestroyableObject.ChangeLightsDamage();
                }


            }
        }

        Destroy(gameObject);
    }


}