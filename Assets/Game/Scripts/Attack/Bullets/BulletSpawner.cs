using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
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
        Debug.Log(bullet.gameObject.activeSelf);
        StartCoroutine(a());
    }

    private Bullet CreateNewBullet()
    {
        GameObject newBullet = Instantiate(_bulletPrefab, spawnPoint.position, Quaternion.identity, _poolBulletList);
        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.onLifeTimeEnd += OnReleaseToPool;
        bullet.gameObject.SetActive(false);
        bullet.SetPool(_objectPool);
        
        return bullet;
    }
    // invoked when returning an item to the object pool
    public void OnGetFromPool(Bullet bullet)
    {
        bullet.Enable();
        bullet.Direction = (_enemyFsm.VisionSystem.PlayerLastPosition - bullet.gameObject.transform.position).normalized;
        bullet.gameObject.transform.SetParent(_worldBulletParent);
    }
    public Bullet GetFromPool()
    {
        if (_objectPool.CountInactive > 0)
        {
            Bullet bullet = _objectPool.Get(); 
            bullet.Enable();
            bullet.Direction = (_enemyFsm.VisionSystem.PlayerLastPosition - bullet.gameObject.transform.position).normalized;
            return bullet;
        }
        
        return CreateNewBullet();
    }

    public void OnReleaseToPool(Bullet bullet)
    { 
        bullet.Disable();
        bullet.gameObject.transform.SetParent(_poolBulletList);
    }
    
    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
