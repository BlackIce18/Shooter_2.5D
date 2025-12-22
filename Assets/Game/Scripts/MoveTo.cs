using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveTo : MonoBehaviour
{
    [SerializeField] private Characteristics _characteristics;
    private Transform _goal;
    private NavMeshAgent agent;
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
