using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArmAI : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;

    // Cashed References
    public Transform player;
    public GameObject enemyProjectile;

    // States 
    public float attackRange;
    public bool playerInAttackRange;
    public bool playerInvisible;


    // Attack State
    public float timeBetweenAttacks;
    public bool alreadyAttacked;



    void Awake()
    {
        player = GameObject.Find("Character").transform;
    }

    void Update()
    {
        // Checking for player in attack range.
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!playerInAttackRange)
        {
            Idle();
        }
        if (playerInAttackRange && playerInvisible)
        {
            Idle();
        }
        else if (playerInAttackRange && !playerInvisible)
        {
            AttackPlayer();
        }
    }


    private void AttackPlayer()
    {
        transform.LookAt(player);

        /*
        if (!alreadyAttacked)
        {
            // Fire Projectile
            Rigidbody rb = Instantiate(enemyProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);


            // Cycling "alreadyAttacked" between true and false so that the enemy does not spam attacks.
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        */
    }


    void ResetAttack()
    {
        alreadyAttacked = false;
    }


    public void Idle()
    {
        transform.LookAt(player, Vector3.up);

    }



    /*
    public void DisableEnemy()
    {
        Destroy(gameObject);
    }
    */




}
