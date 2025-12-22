using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    private readonly List<Effects> _activeEffects = new();

    private void Update()
    {
        float dt = Time.deltaTime;

        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            var effect = _activeEffects[i];
            effect.Tick(dt);

            if (!effect.IsActive)
            {
                Debug.Log($"{effect} is out!");   
                _activeEffects.RemoveAt(i);
            }
        }
    }

    public void AddEffect(Effects effect)
    {
        if (effect == null) return;

        var existing = _activeEffects.Find(e => e.Id == effect.Id);

        if (existing != null && effect.CanStack)
        {
            existing.ResetDuration();
            return;
        }
        
        Effects instance = Instantiate(effect);
        instance.Initialize(gameObject);
        _activeEffects.Add(instance);
    }
}
