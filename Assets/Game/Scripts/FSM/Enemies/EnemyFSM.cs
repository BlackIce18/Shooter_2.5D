using System;
using Game.Scripts;
using UnityEditor;
using UnityEngine;

// EnemyStates, VisionSystem using this class
public class EnemyFSM : MonoBehaviour
{
    public bool InAttackRange;
    public bool CanSeePlayer;
    public StateMachine<EnemyFSM> FSM;
    [SerializeField] private MoveTo _navMeshAgent;
    [SerializeField] private Patrol _patrol;
    [SerializeField] private Transform _player;
    [SerializeField] private VisionSystem _visionSystem;
    public Transform Player { 
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
        FSM = new StateMachine<EnemyFSM>(this);
        FSM.ChangeState(new EnemyIdleState());
    }

    private void Update()
    {
        FSM.Update();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(100,200,050,0.25f);
        Gizmos.DrawSphere(transform.position, 0.6f);
    }
}
