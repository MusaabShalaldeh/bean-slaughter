using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EntityController
{
    [Header("References")]
    public Entity EnemyEntity;
    public NavMeshAgent agent;
    public LayerMask groundLayer;

    [Header("Settings")]
    public float roamRange = 6.0f;
    public float stoppingDistance = 0.5f;
    public Vector2 RestTime = new Vector2 (0.2f, 5);

    // Private Variables
    Vector3 destination;
    bool isWalkPointsSet;
    bool isResting;
    bool hasSpottedPlayer = false;
    bool canMove = true;
    [HideInInspector] public bool stopWhenAttacking;

    void Start()
    {
        agent.speed = EnemyEntity.speed;
    }

    void Update()
    {
        if (!canMove)
            return;

        Debug.Log("enemy controller update");
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!hasSpottedPlayer)
            Patrol();
        else
        {
            if (Vector3.Distance(transform.position, destination) <= stoppingDistance)
            {
                agent.isStopped = true;
                return;
            }

            agent.isStopped = false;
            agent.SetDestination(destination);
        }
    }

    public void CheckForPlayer(Entity player)
    {
        if (player == null) 
        {
            hasSpottedPlayer = false;
            agent.isStopped = false;
            return;
        }

        hasSpottedPlayer = true;
        agent.isStopped = true;

        if (!stopWhenAttacking)
            destination = GetMeleeRangePosition(player.transform);
        else
            destination = transform.position;

        agent.isStopped = false;
    }

    public void PlayerEscaped()
    {
        hasSpottedPlayer = false;
    }

    void Patrol()
    {
        if (isResting) 
            return;

        if (!isWalkPointsSet) SearchForDestination();
        if(isWalkPointsSet) agent.SetDestination(destination);
        if (Vector3.Distance(transform.position, destination) < stoppingDistance) StartCoroutine(Rest(Random.Range(RestTime.x, RestTime.y)));
    }

    void SearchForDestination()
    {
        float x = Random.Range(-roamRange, roamRange);
        float z = Random.Range(-roamRange, roamRange);

        destination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destination, Vector3.down, groundLayer))
            isWalkPointsSet = true;
    }

    IEnumerator Rest(float time)
    {
        isResting = true;

        yield return new WaitForSeconds(time);

        isResting = false;
        isWalkPointsSet = false;
    }

    Vector3 GetMeleeRangePosition(Transform t)
    {
        Vector3 pos = t.position + (transform.forward * -1) * 1.1f;

        return pos;
    }

    public override bool IsMoving()
    {
        if (agent.velocity.sqrMagnitude > 0) 
            return true;
        else
            return false;
    }

    public void EnableMovement()
    {
        canMove = true;
        agent.isStopped = false;
        hasSpottedPlayer = false;

    }

    public void DisableMovement()
    {
        canMove = false;
    }

}
