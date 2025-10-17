using System;
using UnityEngine;

[RequireComponent(typeof(AudioData))]
public class HealthSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private float _health = 30;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioData _audioData;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                EventBus.Publish(new DeathEvent(gameObject));
                EventBus.Publish(new SoundEvent(gameObject, _audioData.soundSet.deathSound));
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        
        EventBus.Publish(new DamageEvent(gameObject, damage, Vector3.zero));
        EventBus.Publish(new SoundEvent(gameObject, _audioData.soundSet.hitSound));
        Debug.Log($"{gameObject.name} получил {damage} урона. Осталось: {Health}");
    }
}
