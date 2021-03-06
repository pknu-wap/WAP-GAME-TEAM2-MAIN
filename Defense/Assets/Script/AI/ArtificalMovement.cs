using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class ArtificalMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private float originMoveSpeed;
    [SerializeField]
    private float originStopDistanceFromDest;

    private float currentMoveSpeed;
    public float MoveSpeed
    {
        get => currentMoveSpeed;
        set => currentMoveSpeed = value;
    }
    public float stoppingDistance { get => navMeshAgent.stoppingDistance; }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = originMoveSpeed;
        navMeshAgent.stoppingDistance = originStopDistanceFromDest;
        currentMoveSpeed = originMoveSpeed;
        navMeshAgent.autoBraking = true;
    }
    public void Init(float speed)
    {
        originMoveSpeed = speed;
        currentMoveSpeed = speed;
    }
    public void InitSpeed()
    {
        currentMoveSpeed = originMoveSpeed;
    }

    public void MoveTo(Transform dest)
    {
        MoveTo(dest.position);
    }

    public void MoveTo(Vector3 dest)
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(dest);

        
    }
}
