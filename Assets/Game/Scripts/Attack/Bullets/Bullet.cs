using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Tooltip("BulletSpawner must use Initialize")]
    [SerializeField] private BulletData _bulletData;
    
    private IObjectPool<Bullet> _objectPool;
    [SerializeField] private Collider _collider;
    [SerializeField] private AttackHitbox _hitbox;
    private Vector3 _direction;

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
    public float Speed => _bulletData.speed;
    public void Initialize(BulletData data)
    {
        _bulletData = data;
    }
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
        
        if (_currentLifeTime >= _bulletData.lifeTime)
        {
            onLifeTimeEnd?.Invoke(this);
            return;
        }
        
        Vector3 _targetVelocity = _direction * _bulletData.speed;
        Debug.DrawRay(transform.position, Direction * 3f, Color.red, 2f);
        _currentVelocity = Vector3.MoveTowards(_currentVelocity, _targetVelocity, (_targetVelocity.magnitude > 0 ? _bulletData.acceleration : _bulletData.deceleration) * Time.fixedDeltaTime);
        if (_bulletData.useAcceleration)
            // Если без ускорения, но точно в цель
            transform.position += _targetVelocity * Time.fixedDeltaTime;
        else
            transform.position += _currentVelocity * Time.fixedDeltaTime;
    }
    private void HandleHit(HealthComponent target)
    {
        _objectPool.Release(this);
        EventBus.Publish(new DamageEvent(target.gameObject, _bulletData.damage, Vector3.zero));
    }
}