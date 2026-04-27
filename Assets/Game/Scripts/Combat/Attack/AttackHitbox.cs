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
        Debug.Log(other.gameObject);
        if(!other.TryGetComponent(out EnemyHitDetector target)) return;
        
        if(!_canDamage.Contains(target.DamageReceiver.TargetType)) {return;}
        
        if (!_hitTargets.Contains(target.HealthComponent))
        {
            _hitTargets.Add(target.HealthComponent);
            OnHit?.Invoke(target.HealthComponent);
        }
    }
}
