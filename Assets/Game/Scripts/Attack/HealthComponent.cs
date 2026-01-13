using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private Characteristics _characteristics;
    public float Current => _characteristics.Current.health;
    public float Max => _characteristics.Base.health;
    public bool IsAlive => _characteristics.Current.health > 0;

    private void Awake()
    {
        _characteristics.Current.health = _characteristics.Base.health;
        if (_characteristics == null) _characteristics = GetComponent<Characteristics>();
    }

    public void Apply(float value)
    {
        if(!IsAlive) return;

        _characteristics.Current.health = Mathf.Clamp(_characteristics.Current.health + value, 0, _characteristics.Base.health);
        Debug.Log($"I'm {gameObject.name} receive damage {value}. Current hp: {_characteristics.Current.health}");
        if (Current <= 0)
        {
            EventBus.Publish(new DeathEvent(gameObject));
        }
    }
}
