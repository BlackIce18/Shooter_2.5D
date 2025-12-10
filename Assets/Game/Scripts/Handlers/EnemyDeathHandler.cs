using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyDeathHandler : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable() => EventBus.Subscribe<DeathEvent>(OnDeath);
    private void OnDisable() => EventBus.Unsubscribe<DeathEvent>(OnDeath);

    private void OnDeath(DeathEvent e)
    {
        if(e.target != gameObject) return;
        if (_navMeshAgent)
        {
            _navMeshAgent.speed = 0;
            _navMeshAgent.destination = gameObject.transform.position;
        }
        var animator = GetComponent<Animator>();
        animator?.SetTrigger("Die");
        
        Destroy(gameObject, 1.5f);
    }
}
