using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator; 

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

        _animator?.SetTrigger("Die");
        
        Destroy(gameObject, 1.5f);
    }
}
