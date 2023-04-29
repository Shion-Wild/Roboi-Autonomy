using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask playerMask;

    // Cashed References
    public NavMeshAgent enemyAgent;
    public Transform player;
    public GameObject enemyProjectile;
    
    // States 
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;
    public bool playerInvisible;

    // Patrol State
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    // Attack State
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    //SFX Clips
    [SerializeField] public AudioClip patrolling;
    //[SerializeField] public AudioClip takeDamage;
    //[SerializeField] public AudioClip death;


    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Character").transform;
    }

    void Start()
    {
        SoundManager.Instance.EnemyPatrollingSound(patrolling);
    }

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
        // Find random Z and X to create a new walk point
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        // Setting new walkpoint with random Z and X points
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
            Rigidbody rb = Instantiate(enemyProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            Destroy(rb.gameObject, 3f);


            // Cycling "alreadyAttacked" between true and false so that the enemy does not spam attacks.
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    
    void ResetAttack()
    {
        alreadyAttacked = false;
    }


    public void IgnorePlayer()
    {
        enemyAgent.SetDestination(-player.position);
    }


    
    public void DisableEnemy()
    {
        Destroy(gameObject);
    }
    



}
