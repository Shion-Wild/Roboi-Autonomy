using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public float enemyHealth;
    //public GameObject enemyProjectile;
    PlayerMovementUpdated playerScript;

    public Transform player;

    public LayerMask groundMask;
    public LayerMask playerMask;

    // Patroling 
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;


    // Attacking 
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    // For now this will just be destroy object on collison enter.

    // States 
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;
    public bool playerInvisible;

    void Start()
    {
        playerScript = GameObject.Find("Updated 3rd Person Player").GetComponent<PlayerMovementUpdated>();

    }

    void Awake()
    {
        player = GameObject.Find("Updated 3rd Person Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        // Checking for player sight range.
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInvisible)
        {
            IgnorePlayer();
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            enemyAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint Reached  
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Find random point in rage
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        enemyAgent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        enemyAgent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Add Enemy Attack Code
            //Rigidbody rb = Instantiate(enemyProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            //alreadyAttacked = true;
            //Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }


    public void DisableEnemy()
    {
        // Code to stop enemy movement or destroy the enemy
        Destroy(gameObject);
    }

    public void IgnorePlayer()
    {
        enemyAgent.SetDestination(-player.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript.PlayDeath();
            Debug.Log("Player is Dead");
        }
        
    }

}
