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
            if (Vector3.Distance(owner.VisionSystem.PlayerLastPosition, owner.transform.position) >= 0.5f)
            {
                owner.NavMeshAgent.MoveToPoint(owner.VisionSystem.PlayerLastPosition);
            }
            else
            {
                owner.FSM.ChangeState(new EnemyIdleState());
            }
            return;
        }

        if (owner.InAttackRange)
        {
            owner.FSM.ChangeState(new EnemyAttackState());
            return;
        }
        owner.NavMeshAgent.Goal = owner.Player;
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
        
        owner.NavMeshAgent.Goal = owner.Patrol.CurrentPoint;
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
        
        owner.Patrol.MoveBetweenPoints();
    }

    public void Exit(EnemyFSM owner)
    {
        Debug.Log($"{owner.name} покидает Patrol");
    }
}

public class EnemyAttackState : IState<EnemyFSM>
{
    private float _attackCooldown;
    public void Enter(EnemyFSM owner)
    {
        Debug.Log($"{owner.name} Attack!");
        _attackCooldown = 1.0f;

        //EventBus.Publish(new SoundEvent(null, owner.));
    }

    public void Update(EnemyFSM owner)
    {
        _attackCooldown -= UnityEngine.Time.deltaTime;

        if (_attackCooldown <= 0)
        {
            if (owner.InAttackRange)
            {
                _attackCooldown = 1.0f;
                EventBus.Publish(new DamageEvent(owner.Player.gameObject, 10, owner.transform.position));
            }
        }
        else
        {
            owner.FSM.ChangeState(new EnemyChaseState());
        }
    }

    public void Exit(EnemyFSM owner)
    {
        Debug.Log($"{owner.name} прекращает Attack");
    }
}