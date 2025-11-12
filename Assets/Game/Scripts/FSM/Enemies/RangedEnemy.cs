using System.Collections;
using UnityEngine;

public class RangedEnemy : EnemyFSM
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private float _safeDistance = 2.5f;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private Animator _animator;
    public override bool InAttackRange => Vector3.Distance(transform.position, PlayerPosition) <= characteristics.Current.attackDistance;

    protected override void Awake()
    {
        base.Awake();
        Fsm.ChangeState(new EnemyIdleState());
    }

    private void Update()
    {
        _attackCooldownTimer -= Time.deltaTime;
        Fsm.Update();

        //base.Update();
        float dist = Vector3.Distance(transform.position, PlayerPosition);
        
        if (VisionSystem.CanSeePlayer)
        {
            if (dist < _safeDistance)
            {
                if(!(Fsm.CurrentState is EnemyRetreatState))
                    Fsm.ChangeState(new EnemyRetreatState(safeDistance: _safeDistance, duration: 1.5f));
            }
            else if (dist <= characteristics.Current.attackDistance && CanAttack)
            {
                if(!(Fsm.CurrentState is EnemyAttackState))
                    Fsm.ChangeState(new EnemyAttackState());
            }
            else if (dist > characteristics.Current.attackDistance)
            {
                if(!(Fsm.CurrentState is EnemyRetreatState))
                    Fsm.ChangeState(new EnemyChaseState());
            }
        }
        else
        {
            if(!(Fsm.CurrentState is EnemyPatrolState))
                Fsm.ChangeState(new EnemyPatrolState());
        }
    }

    public override void MoveTo(Vector3 position)
    {
        NavMeshAgent.MoveToPoint(position);
        //transform.position = Vector3.MoveTowards(transform.position, position, 2f * Time.deltaTime);
    }

    protected override void PerformAttack()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("IsAttack");
            _animator.SetBool("IsAttacking", true);
            _animator.SetFloat("AttackNumber", 0);
            //_animator.SetTrigger("Attack");
        }

        StartCoroutine(ShootProjectile());
    }

    private IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(0.3f); // Задержка под анимацию
        Bullet bullet = _bulletSpawner.GetFromPool();
        /*if (bullet && _bulletSpawner.point)
        {
            var projecTile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            var rigidBody = projecTile.GetComponent<Rigidbody>();
            if (rigidBody != null)
                rigidBody.linearVelocity = (PlayerPosition - _firePoint.position).normalized * _projectileSpeed;

            var damage = projecTile.GetComponent<Projectile>();
            if (damage != null) damage.SetDamage(_damage);
        }*/
    }
}