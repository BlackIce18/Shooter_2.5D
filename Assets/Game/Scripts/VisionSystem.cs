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
    private bool _canSeePlayer;
    private bool _playerInTrigger;
    private Vector3 _playerLastPosition;

    public bool CanSeePlayer => _canSeePlayer;
    public Vector3 PlayerLastPosition => _playerLastPosition;
    public event Action<bool> OnVisibilityChanged;

    private void Awake()
    {
        if (_collider != null)
            _collider.isTrigger = true;
    }

    private void Update()
    {
        bool previousState = _canSeePlayer;
        bool playerVisible = _player && IsPlayerVisible();
        if (playerVisible)
        {
            _canSeePlayer = true;
            _chaseTimer = _chaseTime;
            _playerLastPosition = _player.position;
        }
        else
        {
            if (_chaseTime > 0)
            {
                _chaseTimer -= Time.deltaTime;
                _canSeePlayer = true;
            }
            else
            {
                _canSeePlayer = false;
            }
        }

        if (previousState != _canSeePlayer)
        {
            OnVisibilityChanged?.Invoke(_canSeePlayer);
        }
    }

    private bool IsPlayerVisible()
    {
        if (_player == null) return false;
        if (_playerInTrigger) return true;
        
        Vector3 forwardOffset = transform.forward * 0.05f;
        Vector3 origin = transform.position + Vector3.up * _eyeHeight + forwardOffset;
        Vector3 dirToPlayer = (_player.position - origin).normalized;

        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
        if (angleToPlayer > _viewAngle * 0.5f) return false;
        
        if (Physics.CapsuleCast(origin, origin + Vector3.up * _eyeHeight, _viewRadius, dirToPlayer, out RaycastHit hit, _viewDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInTrigger = false;
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
