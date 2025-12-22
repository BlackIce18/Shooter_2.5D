using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

[RequireComponent(typeof(Patrol))]
public class MovementTrap : Trap
{
    [SerializeField] private Patrol _patrol;
    [SerializeField] private MoveTo _agent;
    protected void Update()
    {
        base.Update();
        _agent.Goal = _patrol.CurrentPoint;
        _patrol.MoveBetweenPoints();
    }
    protected override void PerformAttack(HealthHandler target)
    {
        EventBus.Publish(new DamageEvent(target.gameObject, _damage, Vector3.zero));
    }
}