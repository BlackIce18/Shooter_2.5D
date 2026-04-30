using System;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(AudioData))]
public class HealthHandler : MonoBehaviour
{
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioData _audioData;
    [SerializeField] private TargetType _targetType;

    public TargetType TargetType
    {
        get => _targetType;
    }
    public float Health
    {
        get { return _characteristics.Current.health; }
        set
        {
            _characteristics.Current.health = value;

            if (_characteristics.Current.health <= 0)
            {
                EventBus.Publish(new DeathEvent(gameObject));
                EventBus.Publish(new SoundEvent(gameObject, _audioData.soundSet.deathSound));
            }
        }
    }
    /*
    private void OnEnable() => EventBus.Subscribe<DamageEvent>(OnTakeDamage);
    private void OnDisable() => EventBus.Unsubscribe<DamageEvent>(OnTakeDamage);

    public void OnTakeDamage(DamageEvent e)
    {
        if(e.target != gameObject) return;
        Health -= e.damage;
        _animator.SetTrigger("WasAttacked");
        EventBus.Publish(new SoundEvent(gameObject, _audioData.soundSet.hitSound));
        Debug.Log($"{Health} осталось хп");
    }*/
}
