using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveTo : MonoBehaviour
{
    private Transform _goal;
    private NavMeshAgent agent;
    public Transform Goal
    {
        get => _goal;
        set => _goal = value;
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Move(Transform goal)
    {
        agent.destination = goal.position;
    }

    public void MoveToPoint(Vector3 position)
    {
        agent.destination = position;
    }
    private void Update()
    {
        if(_goal)
            Move(_goal);
    }
}
