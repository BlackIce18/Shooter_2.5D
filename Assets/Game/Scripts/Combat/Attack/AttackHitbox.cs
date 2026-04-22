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

    private void OnEnable() =>_hitTargets.Clear();
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out DamageReceiver target)) return;
        Debug.Log(_canDamage.Contains(target.TargetType));
        if(!_canDamage.Contains(target.TargetType)) return;
        
        if (!other.TryGetComponent(out HealthComponent healthComponent)) return;
        
        if(!_canDamage.Contains(target.TargetType)) {return;}
        
        if (!_hitTargets.Contains(healthComponent))
        {
            _hitTargets.Add(healthComponent);
            OnHit?.Invoke(healthComponent);
        }
    }
}
