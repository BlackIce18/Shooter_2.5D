using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TrapTriggerZone : MonoBehaviour
{
    [SerializeField] private Trap _trap;
    [SerializeField] private TargetType[] _canActivate;
    [SerializeField] private float _activationDelay = 0f;
    [SerializeField] private bool _triggerOnce = false;
    private bool _isActivated;
    private void Awake()
    {
        if (_trap == null)
            _trap = GetComponentInParent<Trap>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out HealthHandler target)) return;
        if(!_canActivate.Contains(target.TargetType)) return;

        if (_activationDelay > 0)
            Invoke(nameof(ActivateTrap), _activationDelay);
        else
            ActivateTrap();
    }
    
    private void OnTriggerStay(Collider other)
    {
        /*if (!_trap.CanAttack) return;
        if(!other.TryGetComponent(out HealthHandler target)) return;
        if(!_canActivate.Contains(target.TargetType)) return;

        if (_activationDelay > 0)
            Invoke(nameof(ActivateTrap), _activationDelay);
        else
            ActivateTrap();*/
    }

    private void ActivateTrap()
    {
        _trap?.OnTriggered();
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (TryGetComponent(out Collider col))
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
    }
#endif
}
