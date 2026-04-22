using System;
using Game.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
public abstract class EnemyFSM : MonoBehaviour, IEnemy
{
    [SerializeField] private MoveTo _navMeshAgent;
    public StateMachine<EnemyFSM> Fsm { get; private set; }
    [SerializeField] private VisionSystem _visionSystem;
    [SerializeField] private Patrol _patrol;
    [SerializeField] private PlayerController _player;
    [SerializeField] protected Collider _collider;
    [SerializeField] protected Characteristics characteristics;
    protected float _attackCooldownTimer;
    public Patrol Patrol => _patrol;
    public VisionSystem VisionSystem => _visionSystem;
    public MoveTo NavMeshAgent => _navMeshAgent;
    public bool CanAttack => _attackCooldownTimer <= 0f;
    public float AttackRate { 
        get => characteristics.Current.attackRate;
        protected set => characteristics.Current.attackRate = value;
    }
    public Transform Transform => transform;
    public Collider Collider => _collider;
    public virtual bool InAttackRange { get; protected set; }
    public virtual bool CanSeePlayer { get; protected set; }
    public virtual Vector3 PlayerPosition => _player.Transform.position;
    public virtual PlayerController Player => _player;
    protected virtual void Awake()
    {
        Fsm = new StateMachine<EnemyFSM>(this);
    }

    protected virtual void Update()
    {
        _attackCooldownTimer -= Time.deltaTime;
        Fsm.Update();
    }

    public abstract void MoveTo(Vector3 position);

    public void Attack()
    {
        if (!CanAttack) return;
        PerformAttack();
        _attackCooldownTimer = AttackRate;
    }

    protected abstract void PerformAttack();

    public virtual void LookAt(Vector3 position)
    {
        Vector3 dir = (position - transform.position).normalized;
        //dir.y = 0;
        if (dir != Vector3.zero)
            transform.forward = dir;
    }
}