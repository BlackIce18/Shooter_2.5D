using System;
using UnityEngine;

public class TriggerTrap : Trap
{
    [SerializeField] private float _retractDelay = 0.5f;
    [SerializeField] private AttackHitbox _damageZone;
    [SerializeField] private Collider _triggerZone;
    protected override void Awake()
    {
        base.Awake();
        if (_damageZone != null)
        {
            _damageZone.OnHit += PerformAttack;
        }
    }

    protected override void PerformAttack(HealthHandler target)
    {
        EventBus.Publish(new DamageEvent(target.gameObject, _damage, transform.position));
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<HealthHandler>() == null) return;
        if(!CanAttack) return;
        
        Animator?.SetTrigger("Extend"); // анимация вылета шипов
        _attackCooldownTimer = AttackRate;
        Invoke(nameof(Retract), _retractDelay);
    }

    private void Retract()
    {
        Animator?.SetTrigger("Retract"); // анимация возврата
    }
}