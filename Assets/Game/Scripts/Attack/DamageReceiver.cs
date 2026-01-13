using System;
using UnityEngine;
public enum TargetType
{
    Enemy,
    Player,
    Destructible, // бочки, ящики, кусты
    Neutral       // NPC
}
public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private TargetType _targetType;
    [SerializeField] private HealthComponent _healthComponent;

    public TargetType TargetType => _targetType;

    private void OnEnable()
    {
        EventBus.Subscribe<DamageEvent>(OnDamage);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<DamageEvent>(OnDamage);
    }

    private void OnDamage(DamageEvent e)
    {
        if(e.target != gameObject) return;

        float finalDamage = CalculateDamage(e);
        _healthComponent.Apply(-finalDamage);
        
        EventBus.Publish(new DamageEvent(gameObject, finalDamage, Vector3.zero));
    }

    private float CalculateDamage(DamageEvent e)
    {
        // броня, резисты, крит
        return e.damage;
    }
}
