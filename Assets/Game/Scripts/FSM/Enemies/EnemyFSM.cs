using System;
using Game.Scripts;
using UnityEditor;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public MoveTo navMeshAgentMover;
    public Patrol patrol;
    public bool InAttackRange;
    public bool CanSeePlayer;
    public StateMachine<EnemyFSM> FSM;
    public Transform player;

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
