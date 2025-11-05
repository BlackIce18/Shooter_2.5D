using System;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackRate = 1f;
    [SerializeField] protected TrapHitBox _hitbox;
    public Animator Animator { get => _animator; }
    protected float _attackCooldownTimer;
    public float AttackRate { 
        get => _attackRate;
        protected set => _attackRate = value;
    }
    public bool CanAttack => _attackCooldownTimer <= 0f;
    protected virtual void Awake()
    {
        _attackCooldownTimer = 0;
    }

    protected virtual void Start()
    {
        if (_hitbox != null)
            _hitbox.OnHit += OnHitHandler;
    }

    protected void Update()
    {
        if(_attackCooldownTimer >= 0)
            _attackCooldownTimer -= Time.deltaTime;
    }

    protected abstract void PerformAttack(HealthHandler target);

    private void OnHitHandler(HealthHandler target)
    {
        if(!CanAttack) return;
        
        PerformAttack(target);
        _attackCooldownTimer = AttackRate;
        
        if(_animator)
            _animator.SetTrigger("IsAttack");

        OnTriggered(target);
    }
    protected virtual void OnTriggered(HealthHandler target) { }
}



public class CycledTrap : Trap
{
    protected override void PerformAttack(HealthHandler target)
    {
        
    }
}
public class MovementTrap : Trap
{
    [SerializeField] private AttackHitbox _attackHitbox;
    [SerializeField] private float _speed;
    [SerializeField] protected float _attackRate = 1f;

    protected override void PerformAttack(HealthHandler target)
    {
        
    }
}

public class ShootingTrap : Trap
{
    [SerializeField] private BulletSpawner _bulletSpawner;

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