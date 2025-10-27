using System;
using UnityEngine;

[RequireComponent(typeof(EnemyFSM))]
public class VisionSystem : MonoBehaviour
{
    [SerializeField] private float _viewDistance = 5f;
    [Range(0, 359)][SerializeField] private float _viewAngle = 120f;
    [SerializeField] private float _viewRadius = 0.3f; // "толщина" луча
    [SerializeField] private float _eyeHeight = 1f;
    [SerializeField] private float _chaseTime = 3f;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private Transform _player;
    private float _chaseTimer;
    private EnemyFSM _enemyFsm;
    private bool _canSeePlayer;

    private Vector3 _playerLastPosition;

    public Vector3 PlayerLastPosition
    {
        get => _playerLastPosition;
        private set => _playerLastPosition = value;
    }

    private void Awake()
    {
        _enemyFsm = GetComponent<EnemyFSM>();
        _collider.isTrigger = true;
        _playerLastPosition = _player.position;
    }

    private void Update()
    {
        if (_canSeePlayer)
        {
            _chaseTimer = _chaseTime;
        }
        else if (_chaseTimer > 0)
        {
            _chaseTimer -= Time.deltaTime;
        }

        if (_player && IsPlayerVisible())
        {
            _canSeePlayer = true;
            _enemyFsm.canSeePlayer = true;
            _playerLastPosition = _player.position;
        }
        else
        {
            _canSeePlayer = false;
            _enemyFsm.canSeePlayer = _chaseTimer > 0;
        }
    }

    private bool IsPlayerVisible()
    {
        if (_player == null) return false;
        
        Vector3 forwardOffset = transform.forward * 0.05f;
        Vector3 origin = transform.position + Vector3.up * _eyeHeight + forwardOffset;
        Vector3 dirToPlayer = (_player.position - origin).normalized;

        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
        if (angleToPlayer > _viewAngle * 0.5f) return false;
        
        // Если игрок вплотную — считаем, что видим его
        float distanceToPlayer = Vector3.Distance(_player.position, transform.position);
        if (distanceToPlayer <= 1f)
            return true;

        
        if (Physics.CapsuleCast(origin, origin + Vector3.up * _eyeHeight, _viewRadius, dirToPlayer, out RaycastHit hit, _viewDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("True");
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collide");
            _canSeePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canSeePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 forwardOffset = transform.forward * 0.05f;
        Vector3 origin = transform.position + Vector3.up * _eyeHeight + forwardOffset;

        // Основной вектор взгляда
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, transform.forward * _viewDistance);

        // Левые и правые границы обзора
        Vector3 leftBoundary = Quaternion.Euler(0, -_viewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, _viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(origin, leftBoundary * _viewDistance);
        Gizmos.DrawRay(origin, rightBoundary * _viewDistance);
    }
}
