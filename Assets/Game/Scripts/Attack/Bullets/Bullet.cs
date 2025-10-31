using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;
    [SerializeField] private Collider _collider;
    [SerializeField] private AttackHitbox _hitbox;
    private Vector3 _direction;
    
    [Header("Smoothing")] 
    [SerializeField] private float _acceleration = 10f; // Как быстро набирается скорость
    [SerializeField] private float _deceleration = 12f; // Как быстро останавливается
    [SerializeField] private bool _useAcceleration = true;
    private IObjectPool<Bullet> _objectPool;

    public void SetPool(IObjectPool<Bullet> _pool)
    {
        _objectPool = _pool;
    }
    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }
    
    public Collider Collider => _collider;
    public event Action<Bullet> onLifeTimeEnd;
    public bool isActive;

    private float _currentLifeTime = 0;
    private Vector3 _currentVelocity;
    public float Speed => _speed;
    private void Start()
    {
        _hitbox.OnHit += HandleHit;
    }

    public void Enable()
    {
        _currentLifeTime = 0;
        gameObject.SetActive(true);
        isActive = true;
    }
    public void Disable()
    {
        _currentLifeTime = 0;
        gameObject.SetActive(false);
        _currentVelocity = Vector3.zero;
        _direction = Vector3.zero;
        
        isActive = false;
    }

    private void FixedUpdate()
    {
        if(!isActive) return;
        
        _currentLifeTime += Time.deltaTime;
        
        if (_currentLifeTime >= _lifeTime)
        {
            onLifeTimeEnd?.Invoke(this);
            return;
        }
        
        Vector3 _targetVelocity = _direction * _speed;
        Debug.DrawRay(transform.position, Direction * 3f, Color.red, 2f);
        _currentVelocity = Vector3.MoveTowards(_currentVelocity, _targetVelocity, (_targetVelocity.magnitude > 0 ? _acceleration : _deceleration) * Time.fixedDeltaTime);
        if (_useAcceleration)
            // Если без ускорения, но точно в цель
            transform.position += _targetVelocity * Time.fixedDeltaTime;
        else
            transform.position += _currentVelocity * Time.fixedDeltaTime;
    }
    private void HandleHit(HealthHandler target)
    {
        _objectPool.Release(this);
        EventBus.Publish(new DamageEvent(target.gameObject, _damage, Vector3.zero));
    }
}