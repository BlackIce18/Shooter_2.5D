using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackHitbox : MonoBehaviour
{
    public event System.Action<HealthComponent> OnHit;
    private HashSet<HealthComponent> _hitTargets = new HashSet<HealthComponent>();
    [SerializeField] private TargetType _ownerTargetType;
    [SerializeField] private TargetType[] _canDamage;

    [SerializeField] private Effects _inAirEffect;
    
    private void OnEnable() =>_hitTargets.Clear();
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out EnemyHitDetector target)) return;
        
        if(!_canDamage.Contains(target.DamageReceiver.TargetType)) {return;}
        
        if (!_hitTargets.Contains(target.HealthComponent))
        {
            _hitTargets.Add(target.HealthComponent);
            
            
            target?.DamageReceiver.EffectsManager.AddEffect(_inAirEffect);
            EventBus.Publish(new InAirEvent(target.gameObject));
            
            
            OnHit?.Invoke(target.HealthComponent);
        }
    }
}
