using System.Collections;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    private float _defaultPitch;

    private void Start()
    {
        _defaultPitch = _source.pitch;
    }

    private void OnEnable()
    {
        EventBus.Subscribe<SoundEvent>(OnSoundEvent);
        EventBus.Subscribe<PitchedSoundEvent>(OnPitchedSoundEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<SoundEvent>(OnSoundEvent);
        EventBus.Unsubscribe<PitchedSoundEvent>(OnPitchedSoundEvent);
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

    private void OnPitchedSoundEvent(PitchedSoundEvent e)
    {
        if (e.audioClip != null)
        {
            _source.pitch = Random.Range(_defaultPitch + e.randomDiaposon.x, _defaultPitch + e.randomDiaposon.y);
            var clip = e.audioClip;
            _source.PlayOneShot(clip);
        }
    }
}