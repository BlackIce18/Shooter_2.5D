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
        if (owner.inAttackRange)
        {
            owner.Fsm.ChangeState(new EnemyAttackState());
            return;
        }
        if (owner.canSeePlayer)
        {
            owner.Fsm.ChangeState(new EnemyChaseState());
            return;
        }

        if (_timer <= 0 && !owner.canSeePlayer && !owner.inAttackRange)
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
    public void Enter(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} преследует игрока");
    }

    public void Update(EnemyFSM owner)
    {
        if (owner.inAttackRange)
        {
            owner.Fsm.ChangeState(new EnemyAttackState());
            return;
        }
        if (!owner.canSeePlayer)
        {
            if (Vector3.Distance(owner.VisionSystem.PlayerLastPosition, owner.transform.position) >= 1f)
            {
                owner.NavMeshAgent.MoveToPoint(owner.VisionSystem.PlayerLastPosition);
            }
            else
            {
                owner.Fsm.ChangeState(new EnemyIdleState());
            }
            return;
        }
        owner.NavMeshAgent.Goal = owner.Player;
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
        if (owner.inAttackRange)
        {
            owner.Fsm.ChangeState(new EnemyAttackState());
            return;
        }
        if (owner.canSeePlayer)
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
        if (owner.canAttack)
        {
            if (owner.inAttackRange)
            {
                // Rotate to Player
                //owner.transform.LookAt(owner.Player);

                _attackCooldown = 1.0f;
                //Debug.Log("AttackState");
                EventBus.Publish(new DamageEvent(owner.Player.gameObject, 1, owner.transform.position));
            }
        }
        else if (owner.inAttackRange &&  owner.canAttack)
        {
            return;
        }
        else
        {
            owner.Fsm.ChangeState(new EnemyChaseState());
        }
    }

    public void Exit(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} прекращает Attack");
    }
}