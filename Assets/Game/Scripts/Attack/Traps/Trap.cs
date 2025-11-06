using System;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackRate = 1f;
    [SerializeField] protected TrapHitBox _hitbox;
    protected float _attackCooldownTimer;
    public bool CanAttack => _attackCooldownTimer <= 0f;
    public Animator Animator => _animator;
    public float AttackRate { 
        get => _attackRate;
        protected set => _attackRate = value;
    }
    protected virtual void Awake()
    {
        _attackCooldownTimer = 0;
        if (_hitbox != null)
            _hitbox.OnHit += OnHitHandler;
    }

    protected void Update()
    {
        if(_attackCooldownTimer > 0)
            _attackCooldownTimer -= Time.deltaTime;
    }

    protected abstract void PerformAttack(HealthHandler target);


    protected virtual void Activate()
    {
        if(_animator)
            _animator.SetTrigger("IsAttack");
    }
    private void OnHitHandler(HealthHandler target)
    {
        Debug.Log(CanAttack);
        if (!CanAttack) return;
        Activate();
        PerformAttack(target);
        _attackCooldownTimer = _attackRate;
    }
    public virtual void OnTriggered()
    {
        if (!CanAttack) return;
        Activate();
    }
}



public class CycledTrap : Trap
{
    protected override void PerformAttack(HealthHandler target)
    {
        
    }
}

public class BumpingTrap : Trap
{
    [SerializeField] private AttackHitbox _attackHitbox;

    protected override void PerformAttack(HealthHandler target)
    {
        
    }
}