using System;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _flashDuration = 0.1f;

    private Color _originalColor;
    private float _timer;
    private bool _flashing;

    private void Awake()
    {
        _originalColor = _renderer.color;
    }

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
        if (e.target == gameObject)
        {
            _renderer.color = new Color(1.5f, 1.5f, 1.5f, 1f);
            _timer = _flashDuration;
            _flashing = true;
        }
    }
}
