using System;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    private void OnEnable()
    {
        EventBus.Subscribe<DamageEvent>(OnDamage);
        EventBus.Subscribe<DeathEvent>(OnDeath);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<DamageEvent>(OnDamage);
        EventBus.Unsubscribe<DeathEvent>(OnDeath);
    }

    private void OnDamage(DamageEvent e)
    {
        if (e.target.TryGetComponent(out AudioData audioData))
        {
            var clip = audioData.soundSet?.hitSound;
            if (clip != null)
            {
                _source.PlayOneShot(clip);
            }
        }
    }

    private void OnDeath(DeathEvent e)
    {
        if(e.target.TryGetComponent(out AudioData enemyAudio))
        {
            var clip = enemyAudio.soundSet?.deathSound;
            if(clip != null) 
                _source.PlayOneShot(clip);
        }
    }
}
