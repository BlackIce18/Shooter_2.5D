using System;
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

    private void Start()
    {
        _hitbox.OnHit += HandleHit;
    }

    public void Enable()
    {
        _currentLifeTime = 0;
        gameObject.SetActive(true);
        _currentVelocity = Vector3.zero;
        isActive = true;
    }
    public void Disable()
    {
        _currentLifeTime = 0;
        gameObject.SetActive(false);
        _direction = Vector3.zero;
        _currentVelocity = Vector3.zero;
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
        
        Vector3 moveDir = _direction.normalized;
        Debug.Log(moveDir);
        Vector3 _targetVelocity = moveDir * _speed;
        
        _currentVelocity = Vector3.MoveTowards(_currentVelocity, _targetVelocity, (_targetVelocity.magnitude > 0 ? _acceleration : _deceleration) * Time.fixedDeltaTime);
        
        transform.position += _currentVelocity * Time.fixedDeltaTime;
    }
    private void HandleHit(HealthHandler target)
    {
        _objectPool.Release(this);
        EventBus.Publish(new DamageEvent(target.gameObject, _damage, Vector3.zero));
    }
}