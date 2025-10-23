using Game.Scripts;
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
}
