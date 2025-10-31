using UnityEngine;
public class EnemyIdleState : IState<EnemyFSM>
{
    private float _timer;
    public void Enter(EnemyFSM owner)
    {
        _timer = Random.Range(1f, 3f);
        //Debug.Log($"{owner.name} вошел в стостояние IDLE");
    }

    public void Update(EnemyFSM owner)
    {
        _timer -= Time.deltaTime;
        if (owner.InAttackRange)
        {
            owner.Fsm.ChangeState(new EnemyAttackState());
            return;
        }
        if (owner.CanSeePlayer)
        {
            owner.LookAt(owner.PlayerPosition);
            owner.Fsm.ChangeState(new EnemyChaseState());
            return;
        }

        if (_timer <= 0 && !owner.CanSeePlayer && !owner.InAttackRange)
        {
            owner.Fsm.ChangeState(new EnemyPatrolState());
        }
    }

    public void Exit(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} покидает IDLE");
    }
}
public class EnemyChaseState : IState<EnemyFSM>
{
    private bool _movingToLastPos;
    public void Enter(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} преследует игрока");
        _movingToLastPos = false;
    }

    public void Update(EnemyFSM owner)
    {
        if (owner.InAttackRange)
        {
            owner.Fsm.ChangeState(new EnemyAttackState());
            return;
        }
        if (!owner.CanSeePlayer)
        {
            if (!_movingToLastPos)
            {
                owner.MoveTo(owner.VisionSystem.PlayerLastPosition);
                _movingToLastPos = true;
            }
            if (Vector3.Distance(owner.VisionSystem.PlayerLastPosition, owner.transform.position) <= 1f)
            {
                owner.Fsm.ChangeState(new EnemyIdleState());
                
            }
            return;
        }
        _movingToLastPos = false;
        owner.NavMeshAgent.Goal = owner.Player.transform;
    }

    public void Exit(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} покидает Chase");
    }
}
public class EnemyPatrolState : IState<EnemyFSM>
{
    public void Enter(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} начинает патрулирование");
        
        owner.NavMeshAgent.Goal = owner.Patrol.CurrentPoint;
    }

    public void Update(EnemyFSM owner)
    {
        if (owner.InAttackRange)
        {
            owner.Fsm.ChangeState(new EnemyAttackState());
            return;
        }
        if (owner.CanSeePlayer)
        {
            owner.Fsm.ChangeState(new EnemyChaseState());
            return;
        }
        
        owner.Patrol.MoveBetweenPoints();
    }

    public void Exit(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} покидает Patrol");
    }
}

public class EnemyAttackState : IState<EnemyFSM>
{
    private float _attackCooldown;
    public void Enter(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} Attack!");
        _attackCooldown = 1.0f;
    }

    public void Update(EnemyFSM owner)
    {
        if (!owner.InAttackRange)
        {
            owner.Fsm.ChangeState(new EnemyChaseState());
            return;
        }
        owner.Attack();
    }

    public void Exit(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} прекращает Attack");
    }
}