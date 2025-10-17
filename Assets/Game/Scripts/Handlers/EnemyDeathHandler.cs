using System;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    private void OnEnable() => EventBus.Subscribe<DeathEvent>(OnDeath);
    private void OnDisable() => EventBus.Unsubscribe<DeathEvent>(OnDeath);

    private void OnDeath(DeathEvent e)
    {
        if(e.target == null) return;

        var animator = GetComponent<Animator>();
        animator?.SetTrigger("Die");
        
        Destroy(gameObject, 1.5f);
    }
}
