using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float sightRange;
    [SerializeField] private float walkRange;

    private NavMeshAgent enemyAgent;

    private Vector3 walkPoint;
    private bool walkPointSet;

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (IsPlayerInSight()) MoveToTarget(playerTransform.position);
        else Patrol();
    }

    private void MoveToTarget(Vector3 target)
    {
        enemyAgent.SetDestination(target);
    }

    private bool IsPlayerInSight()
    {
        Vector3 direction = playerTransform.position - transform.position;
        
        Debug.DrawLine(transform.position, transform.position + direction.normalized * sightRange, Color.magenta);
        Debug.DrawLine(transform.position + Vector3.up, transform.position + direction.normalized * direction.magnitude, Color.blue);

        bool player = Physics.Raycast(transform.position, direction, sightRange, playerMask);
        bool obstacle = Physics.Raycast(transform.position, direction, direction.magnitude, ~playerMask);

        return !(!player | obstacle);
    }

    private void Patrol()
    {
        Vector3 directionToPoint = walkPoint - transform.position;
        if (directionToPoint.magnitude < 1f) walkPointSet = false;

        if (walkPointSet) MoveToTarget(walkPoint);
        else SearchPoint();
    }

    private void SearchPoint()
    {
        float randomOffsetX = Random.Range(-walkRange, walkRange);
        float randomOffsetZ = Random.Range(-walkRange, walkRange);
        walkPoint = new Vector3(transform.position.x + randomOffsetX, transform.position.y,
            transform.position.z + randomOffsetZ);

        NavMeshPath path = new NavMeshPath();
        enemyAgent.CalculatePath(walkPoint, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            walkPointSet = true;
            Debug.DrawLine(walkPoint, walkPoint + Vector3.up, Color.green, 3f);
        }
        else Debug.DrawLine(walkPoint, walkPoint + Vector3.up, Color.red, 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(walkRange * 2, transform.position.y, walkRange * 2));
    }
}
