using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveTo : MonoBehaviour
{
    [SerializeField] private Characteristics _characteristics;
    private Transform _goal;
    private NavMeshAgent agent;
    private Vector3 _lastTargetPos;
    public Transform Goal
    {
        get => _goal;
        set => _goal = value;
    }

    public float MoveSpeed
    {
        get => _characteristics.Current.speed;
        set => _characteristics.Current.speed = value;
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _characteristics.Current.speed;
    }

    public void Move(Transform goal)
    {
        if (goal == null) Debug.Log("Goal null");
        SetDestinationSafe(goal.position);
    }

    public void MoveToPoint(Vector3 position)
    {
        SetDestinationSafe(position);
    }
    private void SetDestinationSafe(Vector3 position)
    {
        if (!agent || !agent.enabled || !agent.isOnNavMesh)
            return;

        agent.SetDestination(position);
    }
    private void Update()
    {
        if (!_goal || !agent.enabled || !agent.isOnNavMesh)
            return;
        
        if ((_goal.position - _lastTargetPos).sqrMagnitude > 0.01f)
        {
            _lastTargetPos = _goal.position;
            SetDestinationSafe(_lastTargetPos);
        }
        
    }
}
