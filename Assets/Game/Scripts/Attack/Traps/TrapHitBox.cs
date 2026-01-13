using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapHitBox : MonoBehaviour
{
    public event Action<HealthComponent> OnHit;
    [SerializeField] private TargetType[] _canDamage;
    [SerializeField] private float _damageInterval = 1f;

    private Dictionary<HealthComponent, float> _timers = new();

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out DamageReceiver target)) return;
        if(!_canDamage.Contains(target.TargetType)) return;
        
        if(!other.TryGetComponent(out HealthComponent healthComponent)) return;
        if (!_timers.ContainsKey(healthComponent))
        {
            _timers[healthComponent] = _damageInterval;
            Debug.Log("Enter");
            OnHit?.Invoke(healthComponent);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(!other.TryGetComponent(out DamageReceiver target)) return;
        if(!_canDamage.Contains(target.TargetType)) return;

        if(!other.TryGetComponent(out HealthComponent healthComponent)) return;
        if (!_timers.ContainsKey(healthComponent))
        {
            _timers[healthComponent] = _damageInterval;
            Debug.Log("Stay");
            OnHit?.Invoke(healthComponent);
            return;
        }
        
        _timers[healthComponent] -= Time.deltaTime;

        if (_timers[healthComponent] <= 0f)
        {
            _timers[healthComponent] = _damageInterval;
            Debug.Log("Stay1");
            OnHit?.Invoke(healthComponent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out HealthComponent target))
            _timers.Remove(target);
    }
}
