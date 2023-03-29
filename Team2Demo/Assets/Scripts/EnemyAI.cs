using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent enemyAgent;

    public Transform player;

    public LayerMask groundMask;
    public LayerMask playerMask;

    // Patroling 
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    /*
    // Attacking 
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    // For now this will just be destroy object on collison enter.
    */

    // States 
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    //public bool playerInAttackRange;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checking for player sight range.
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);

        if (!playerInSightRange)
        {
            Patrolling();
        }
        else if (playerInSightRange)
        {
            ChasePlayer();
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

        // Don't rape player 
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void ChasePlayer()
    {
        enemyAgent.SetDestination(player.position);

    }

    /*
    // Still figuring out what the final attack mechanics will be
    private void AttackPlayer()
    {

    }
    */
}
