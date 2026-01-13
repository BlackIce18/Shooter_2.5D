using UnityEngine;

public class StaticTrap : Trap
{
    protected override void PerformAttack(HealthComponent target)
    {
        EventBus.Publish(new DamageEvent(target.gameObject, _damage, Vector3.zero));
    }
    
}