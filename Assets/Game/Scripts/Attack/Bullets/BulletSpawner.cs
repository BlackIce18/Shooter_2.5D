using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _poolBulletList;
    [SerializeField] private Transform _worldBulletParent;
    [SerializeField] private List<Bullet> _activeBullets;
    private IObjectPool<Bullet> _objectPool;
    [SerializeField] private int _defaultCapacity = 20;
    [SerializeField] private int _maxPoolCount = 100;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private EnemyFSM _enemyFsm;
    
    // throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;
    private void Awake()
    {
        _objectPool = new ObjectPool<Bullet>(
            CreateNewBullet,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            collectionCheck,
            _defaultCapacity,
            _maxPoolCount);
        
        InitPoolElements();
    }
    private void InitPoolElements()
    {   
        var tempList = new List<Bullet>();
        for (int i = 0; i < _defaultCapacity; i++)
        {
            tempList.Add(GetFromPool());
        }

        foreach (var obj in tempList)
        {
            _objectPool.Release(obj);
        }

        StartCoroutine(a());
    }

    private IEnumerator a()
    {
        yield return new WaitForSeconds(2);
        Bullet bullet = GetFromPool();
        StartCoroutine(a());
    }

    private Bullet CreateNewBullet()
    {
        GameObject newBullet = Instantiate(_bulletPrefab, _spawnPoint.position, Quaternion.identity, _poolBulletList);
        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.onLifeTimeEnd += OnBulletLifeTimeEnd;
        bullet.gameObject.SetActive(false);
        bullet.SetPool(_objectPool);
        
        return bullet;
    }
    // invoked when returning an item to the object pool
    public void OnGetFromPool(Bullet bullet)
    {
        Vector3 spawnPos = _spawnPoint.position;
        bullet.transform.position = spawnPos;
        Collider targetCollider = _enemyFsm.Player.Collider;
        Debug.Log(targetCollider.transform.position);
        Vector3 targetCenter = targetCollider.bounds.center;
        //Vector3 targetCenter = _enemyFsm.Player.gameObject.transform.position;

        float distance = Vector3.Distance(spawnPos, targetCenter);
        float timeToTarget = distance / bullet.Speed;
        
        Vector3 predictedPos = targetCenter + _enemyFsm.Player.CurrentVelocity * timeToTarget;
        bullet.Direction = (predictedPos - spawnPos).normalized;
        bullet.gameObject.transform.SetParent(_worldBulletParent);
        bullet.Enable();
    }
    public Bullet GetFromPool()
    {
        if (_objectPool.CountInactive > 0)
        {
            return _objectPool.Get();
        }
        
        return CreateNewBullet();
    }

    public void OnReleaseToPool(Bullet bullet)
    { 
        bullet.Disable();
        bullet.gameObject.transform.SetParent(_poolBulletList);
    }

    public void OnBulletLifeTimeEnd(Bullet bullet)
    {
        _objectPool.Release(bullet);
    }
    
    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
