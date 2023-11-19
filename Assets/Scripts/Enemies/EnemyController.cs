using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Entity EnemyEntity;
    public NavMeshAgent agent;
    public LayerMask groundLayer;

    [Header("Settings")]
    public float roamRange = 6.0f;
    public float stoppingDistance = 0.5f;

    // Private Variables
    Vector3 destination;
    bool isWalkPointsSet;

    void Start()
    {
        agent.speed = EnemyEntity.speed;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (!isWalkPointsSet) SearchForDestination();
        if(isWalkPointsSet) agent.SetDestination(destination);
        if (Vector3.Distance(transform.position, destination) < stoppingDistance) isWalkPointsSet = false;
    }

    void SearchForDestination()
    {
        float x = Random.Range(-roamRange, roamRange);
        float z = Random.Range(-roamRange, roamRange);

        destination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destination, Vector3.down, groundLayer))
            isWalkPointsSet = true;
    }

    public bool IsMoving()
    {
        if (agent.velocity.sqrMagnitude > 0) 
            return true;
        else
            return false;
    }
}
