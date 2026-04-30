using System;
using Unity.VisualScripting;
using UnityEngine;

public class DamageReaction : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioData _audio;
    [SerializeField] private EnemyHitDetector _enemyHitDetector;

    private void OnEnable()
    {
        EventBus.Subscribe<DamageEvent>(OnHit);
        EventBus.Subscribe<DeathEvent>(OnDeath);
        EventBus.Subscribe<InAirEvent>(OnAir);
        EventBus.Subscribe<InAirOutEvent>(OnAirOut);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<DamageEvent>(OnHit);
        EventBus.Unsubscribe<DeathEvent>(OnDeath);
        EventBus.Unsubscribe<InAirEvent>(OnAir);
        EventBus.Unsubscribe<InAirOutEvent>(OnAirOut);
    }

    private void OnHit(DamageEvent e)
    {
        if(e.target != gameObject) return;
        
        _animator?.SetTrigger("WasAttacked");
        EventBus.Publish(new PitchedSoundEvent(gameObject, _audio.soundSet.hitSound, _audio.pitchSoundOffsets));
    }

    private void OnDeath(DeathEvent e)
    {
        if(e.target != gameObject) return;
        
        _animator?.SetTrigger("Die");
        EventBus.Publish(new PitchedSoundEvent(gameObject, _audio.soundSet.deathSound, _audio.pitchSoundOffsets));
    }
    
    private void OnAir(InAirEvent e)
    {
        if(e.target != _enemyHitDetector?.gameObject) return;
        
        _animator?.SetBool("InAir", true);
        //EventBus.Publish(new PitchedSoundEvent(gameObject, _audio.soundSet.hitSound, _audio.pitchSoundOffsets));
    }

    private void OnAirOut(InAirOutEvent effect)
    {
        _animator?.SetBool("InAir", false);
    }
}
