using UnityEngine;
[RequireComponent(typeof(BulletSpawner))]
public class ShootingTrap : Trap
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _attackDirection;
    protected void Update()
    {
        if(_attackCooldownTimer > 0)
            _attackCooldownTimer -= Time.deltaTime;
        else
        {
            _bulletSpawner.ShootAtTarget(_attackDirection, Vector3.zero);
            _attackCooldownTimer = _attackRate;
        }
    }
    protected override void PerformAttack(HealthHandler target)
    {
        
    }
}
