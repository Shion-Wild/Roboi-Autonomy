using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EMPGrenade : MonoBehaviour
{

    public float delay = 2f;
    public float force = 800f;
    public float radius = 5f;

    // Layer Mask for EMP Testing
    public int layerMask = 1 << 9;

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

                    // Call a method to disable the enemy's AI
                    enemyScript.DisableEnemy();
                }

            } else if (nearbyObject.CompareTag("EMPDoor")) {

                // Get the components of the nearby object
                DestroyableObject destroyableObject = nearbyObject.GetComponent<DestroyableObject>();

                // If the nearby object has a BossDestroyableObject component
                if (destroyableObject != null)
                {
                    // Call a method to destroy the object as part of an EMP effect
                    destroyableObject.EMPDestroyDoor();
                }

            } else if (nearbyObject.CompareTag("EMPBossHit")) {

                // Get the components of the nearby object
                BossDestroyableObject bossDestroyableObject = nearbyObject.GetComponent<BossDestroyableObject>();

                // If the nearby object has a BossDestroyableObject component
                if (bossDestroyableObject != null)
                {
                    // Call a method to destroy the object as part of an EMP effect
                    bossDestroyableObject.EMPDestroyBoss();
                }

            }
        }

        Destroy(gameObject);
    }


}
