using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    private Flashlight flashlight;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet; // check if the walkpoint already set
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;



    private void Awake()
    {
        player = GameObject.Find("FlashLightObject").transform;
        agent = GetComponent<NavMeshAgent>();

        flashlight = FindObjectOfType<Flashlight>(); // Finds the flashlight script in the scene

    }

    private void Update()
    {
        //check if player is in attack or chase range (ändra till ficklampelogik sen)
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Determine behavior based on flashlight state
        if (!flashlight.on)
        {
            // Flashlight is off, enemy should patrol
            Patrolling();
        }
        else
        {
            // Flashlight is on, enemy should chase or attack the player
            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }
            else if (playerInAttackRange && playerInSightRange)
            {
                AttackPlayer();
            }
        }

    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached if the distance is less the 1 you have reached a walkpoint
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        //calculates random points in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //check if the walkpoint is on the ground/within bounds
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
        
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        //TODO lägg till logik om fickampan
    }
    private void AttackPlayer()
    {
        //om man inte vill att enmy ska röra på sig när dne attackerar 
        //agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //add more attack logic here to hurt player
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
