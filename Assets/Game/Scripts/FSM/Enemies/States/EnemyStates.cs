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
    public void Enter(EnemyFSM owner)
    {
        //Debug.Log($"{owner.name} Attack!");
        // сюда можно поместить, например, анимацию атаки старта
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

public class EnemyRetreatState : IState<EnemyFSM>
{
    private readonly float? _safeDistance; // Опциональная дистанция до игрока
    private readonly float? _duration; // Опциональное время отступления
    private readonly float _speedMultiplier; // Можно ускорять врага при отступлении
    private float _timer;

    public EnemyRetreatState(float? safeDistance = null, float? duration = null, float speedMultiplier = 1.2f)
    {
        _safeDistance = safeDistance;
        _duration = duration;
        _speedMultiplier = speedMultiplier;
    }
    public void Enter(EnemyFSM owner)
    {
        _timer = _duration ?? float.MaxValue;
        owner.NavMeshAgent.MoveSpeed *= _speedMultiplier;
    }

    public void Update(EnemyFSM owner)
    {
        Vector3 dir = (owner.transform.position - owner.PlayerPosition).normalized;
        Vector3 retreatPoint = owner.transform.position + dir * 2f;
        owner.MoveTo(retreatPoint);

        _timer -= Time.deltaTime;
        
        float dist = Vector3.Distance(owner.transform.position, owner.PlayerPosition);
        bool safeReached = _safeDistance.HasValue && dist >= _safeDistance.Value;
        bool timeExpired = _duration.HasValue && _timer <= 0f;

        if (safeReached || timeExpired)
        {
            owner.Fsm.ChangeState(new EnemyAttackState());
        }
    }

    public void Exit(EnemyFSM owner)
    {
        owner.NavMeshAgent.MoveSpeed /= _speedMultiplier;
    }
}