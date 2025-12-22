using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapHitBox : MonoBehaviour
{
    public event Action<HealthHandler> OnHit;
    [SerializeField] private TargetType[] _canDamage;
    [SerializeField] private float _damageInterval = 1f;

    private Dictionary<HealthHandler, float> _timers = new();

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out HealthHandler target)) return;
        if(!_canDamage.Contains(target.TargetType)) return;
        
        if (!_timers.ContainsKey(target))
        {
            _timers[target] = _damageInterval;
            Debug.Log("Enter");
            OnHit?.Invoke(target);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(!other.TryGetComponent(out HealthHandler target)) return;
        if(!_canDamage.Contains(target.TargetType)) return;

        if (!_timers.ContainsKey(target))
        {
            _timers[target] = _damageInterval;
            Debug.Log("Stay");
            OnHit?.Invoke(target);
            return;
        }
        
        _timers[target] -= Time.deltaTime;

        if (_timers[target] <= 0f)
        {
            _timers[target] = _damageInterval;
            Debug.Log("Stay1");
            OnHit?.Invoke(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out HealthHandler target))
            _timers.Remove(target);
    }
}
