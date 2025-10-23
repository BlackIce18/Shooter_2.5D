using System;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    private void OnEnable()
    {
        EventBus.Subscribe<SoundEvent>(OnSoundEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<SoundEvent>(OnSoundEvent);
    }

    private void OnSoundEvent(SoundEvent e)
    {
        if (e.audioClip != null)
        {
            var clip = e.audioClip;
            _source.PlayOneShot(clip);
        }
        else if (e.target.TryGetComponent(out AudioData audioData))
        {
            var clip = audioData.soundSet?.hitSound;
            if (clip != null)
            {
                _source.PlayOneShot(clip);
            }
        }
    }
}
