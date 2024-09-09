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
        player = GameObject.Find("").transform;
        agent = GetComponent<NavMeshAgent>();

        flashlight = FindObjectOfType<Flashlight>(); // Finds the flashlight script in the scene

    }

   
}
