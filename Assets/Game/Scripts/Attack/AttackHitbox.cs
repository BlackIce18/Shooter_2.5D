using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public event System.Action<HealthHandler> OnHit;
    private HashSet<HealthHandler> _hitTargets = new HashSet<HealthHandler>();

    private void OnEnable()
    {
        _hitTargets.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthHandler target))
        {
            if (!_hitTargets.Contains(target))
            {
                _hitTargets.Add(target);
                OnHit?.Invoke(target);
            }
        }
    }
}
