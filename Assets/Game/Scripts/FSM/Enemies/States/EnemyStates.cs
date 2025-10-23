using UnityEngine;
public class EnemyIdleState : IState<EnemyFSM>
{
    private float _timer;
    public void Enter(EnemyFSM owner)
    {
        _timer = Random.Range(1f, 3f);
        Debug.Log($"{owner.name} вошел в стостояние IDLE");
    }

    public void Update(EnemyFSM owner)
    {
        _timer -= UnityEngine.Time.deltaTime;

        if (owner.CanSeePlayer)
        {
            owner.FSM.ChangeState(new EnemyChaseState());
            return;
        }

        if (_timer <= 0)
        {
            owner.FSM.ChangeState(new EnemyPatrolState());
        }
    }

    public void Exit(EnemyFSM owner)
    {
        Debug.Log($"{owner.name} покидает IDLE");
    }
}
public class EnemyChaseState : IState<EnemyFSM>
{
    public void Enter(EnemyFSM owner)
    {
        Debug.Log($"{owner.name} преследует игрока");
    }

    public void Update(EnemyFSM owner)
    {
        if (!owner.CanSeePlayer)
        {
            owner.FSM.ChangeState(new EnemyIdleState());
            return;
        }

        if (owner.InAttackRange)
        {
            owner.FSM.ChangeState(new EnemyChaseState());
            return;
        }

        owner.navMeshAgentMover.Goal = owner.player;
    }

    public void Exit(EnemyFSM owner)
    {
        Debug.Log($"{owner.name} покидает Chase");
    }
}
public class EnemyPatrolState : IState<EnemyFSM>
{
    public void Enter(EnemyFSM owner)
    {
        Debug.Log($"{owner.name} начинает патрулирование");
        
        owner.navMeshAgentMover.Goal = owner.patrol.CurrentPoint;
    }

    public void Update(EnemyFSM owner)
    {
        if (owner.CanSeePlayer)
        {
            owner.FSM.ChangeState(new EnemyChaseState());
            return;
        }

        if (owner.InAttackRange)
        {
            owner.FSM.ChangeState(new EnemyChaseState());
            return;
        }
        
        owner.patrol.MoveBetweenPoints();
    }

    public void Exit(EnemyFSM owner)
    {
        Debug.Log($"{owner.name} покидает Patrol");
    }
}