using UnityEngine;
[CreateAssetMenu(menuName = "Effects/Burn")]
public class BurnEffect : Effects
{
    [SerializeField] private float _tickInterval = 1f;
    [SerializeField] private float _damagePerSecond = 1f;
    private float _tickTimer;
    private HealthComponent _target;
    protected override void OnApply()
    {
        _tickTimer = _tickInterval;
        _target = Owner.GetComponent<HealthComponent>();
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (_target == null || !IsActive) return;
        _tickTimer -= deltaTime;

        if (_tickTimer <= 0)
        {
            EventBus.Publish(new DamageEvent(_target.gameObject, _damagePerSecond, Vector3.zero));
            _tickTimer = _tickInterval;
        }
        
        
    }

    protected override void OnEnd()
    {
        Debug.Log($"Effect ended on {Owner.name}");
    }
}
