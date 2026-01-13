using System;
using UnityEngine;

[RequireComponent(typeof(AudioData))]
public class DamageReaction : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioData _audio;

    private void OnEnable()
    {
        EventBus.Subscribe<DamageEvent>(OnHit);
        EventBus.Subscribe<DeathEvent>(OnDeath);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<DamageEvent>(OnHit);
        EventBus.Unsubscribe<DeathEvent>(OnDeath);
    }

    private void OnHit(DamageEvent e)
    {
        if(e.target != gameObject) return;
        
        _animator?.SetTrigger("WasAttacked");
        EventBus.Publish(new SoundEvent(gameObject, _audio.soundSet.hitSound));
    }

    private void OnDeath(DeathEvent e)
    {
        if(e.target != gameObject) return;
        
        _animator?.SetTrigger("Die");
        EventBus.Publish(new SoundEvent(gameObject, _audio.soundSet.deathSound));
    }
}
