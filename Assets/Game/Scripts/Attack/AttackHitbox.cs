using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackHitbox : MonoBehaviour
{
    public event System.Action<HealthHandler> OnHit;
    private HashSet<HealthHandler> _hitTargets = new HashSet<HealthHandler>();
    [SerializeField] private TargetType _ownerTargetType;
    [SerializeField] private TargetType[] _canDamage;

    private void OnEnable() =>_hitTargets.Clear();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthHandler target))
        {
            if(target.TargetType == _ownerTargetType) {return;}
            
            if(!_canDamage.Contains(target.TargetType)) {return;}
            
            if (!_hitTargets.Contains(target))
            {
                _hitTargets.Add(target);
                OnHit?.Invoke(target);
            }
        }
    }
}
