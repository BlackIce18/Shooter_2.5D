using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveTo : MonoBehaviour
{
    private Transform _goal;
    private NavMeshAgent agent;
    [SerializeField] private float _moveSpeed = 1f;
    public Transform Goal
    {
        get => _goal;
        set => _goal = value;
    }

    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _moveSpeed;
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
