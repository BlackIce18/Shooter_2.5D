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

    private void Update()
    {
        agent.destination = _goal.position;
    }
}
