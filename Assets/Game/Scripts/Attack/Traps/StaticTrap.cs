using UnityEngine;

public class StaticTrap : Trap
{
    protected override void PerformAttack(HealthHandler target)
    {
        EventBus.Publish(new DamageEvent(target.gameObject, _damage, Vector3.zero));
    }
    
}