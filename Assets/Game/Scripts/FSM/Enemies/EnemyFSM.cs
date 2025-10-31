using System;
using Game.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemy
{
    Transform Transform { get; }
    Collider Collider { get; }
    bool CanSeePlayer { get; }
    bool InAttackRange { get; }
    Vector3 PlayerPosition { get; }
    void MoveTo(Vector3 position);
    void Attack();
    void LookAt(Vector3 position);
}

public abstract class EnemyFSM : MonoBehaviour, IEnemy
{
    [SerializeField] private MoveTo _navMeshAgent;
    public StateMachine<EnemyFSM> Fsm { get; private set; }
    [SerializeField] private VisionSystem _visionSystem;
    [SerializeField] private Patrol _patrol;
    [SerializeField] private PlayerController _player;
    [SerializeField] protected Collider _collider;
    protected float _attackCooldownTimer;
    public Patrol Patrol => _patrol;
    public VisionSystem VisionSystem => _visionSystem;
    public MoveTo NavMeshAgent => _navMeshAgent;
    public bool CanAttack => _attackCooldownTimer <= 0f;
    public float AttackRate { get; protected set; } = 1f;
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
/*
public class EnemyFSM : MonoBehaviour
{
    public StateMachine<EnemyFSM> Fsm;

    public bool inAttackRange;
    public bool canSeePlayer;
    public bool canAttack = true;
    [SerializeField] private MoveTo _navMeshAgent;
    [SerializeField] private Patrol _patrol;
    [SerializeField] private PlayerController _player;
    [SerializeField] private VisionSystem _visionSystem;

    public PlayerController Player
    {
        get => _player;
        private set => _player = value;
    }

    public MoveTo NavMeshAgent
    {
        get => _navMeshAgent;
        private set => _navMeshAgent = value;
    }

    public Patrol Patrol
    {
        get => _patrol;
        private set => _patrol = value;
    }

    public VisionSystem VisionSystem
    {
        get => _visionSystem;
        private set => _visionSystem = value;
    }

    private void Start()
    {
        Fsm = new StateMachine<EnemyFSM>(this);
        Fsm.ChangeState(new EnemyIdleState());
    }

    private void Update()
    {
        Fsm.Update();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(100, 200, 050, 0.25f);
        Gizmos.DrawSphere(transform.position, 0.6f);
    }
}*/