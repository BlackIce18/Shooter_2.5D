using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public event System.Action<IDamagable> OnHit;

    private HashSet<IDamagable> _hitTargets = new HashSet<IDamagable>();

    private void OnEnable()
    {
        _hitTargets.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable target))
        {
            if (!_hitTargets.Contains(target))
            {
                _hitTargets.Add(target);
                OnHit?.Invoke(target);
            }
        }
    }
}
