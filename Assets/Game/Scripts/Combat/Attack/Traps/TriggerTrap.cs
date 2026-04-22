using System;
using UnityEngine;

public class TriggerTrap : Trap
{
    [SerializeField] private float _activeDuration = 1.5f;
    [SerializeField] private float _retractDelay = 0.5f;
    [SerializeField] private float _damageDelay = 0.15f; // момент удара после появления шипов
    private bool _isActive;
    
    protected override void Activate()
    {
        if (_isActive) return;
        _isActive = true;
        Animator?.SetTrigger("Activate");
        
        if(_hitbox) _hitbox.gameObject.SetActive(true);
        Invoke(nameof(EnableDamage), _damageDelay);
        Invoke(nameof(Deactivate), _activeDuration);
    }

    protected override void PerformAttack(HealthComponent target)
    {
        EventBus.Publish(new DamageEvent(target.gameObject, _damage, transform.position));
    }
    private void EnableDamage()
    {
        _attackCooldownTimer = 0f;
    }
    private void Deactivate()
    {
        _isActive = false;
        Animator?.SetTrigger("Deactivate"); // анимация возврата
        
        _hitbox?.gameObject.SetActive(false);
        
        Invoke(nameof(ResetTrap), _retractDelay);
    }
    private void ResetTrap()
    {
        _attackCooldownTimer = 0f;
    }
}