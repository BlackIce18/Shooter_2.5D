using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Characteristics characteristics;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [Header("Smoothing")] 
    [SerializeField] private float _acceleration = 10f; // Как быстро набирается скорость
    [SerializeField] private float _deceleration = 12f; // Как быстро останавливается
    
    private Vector3 _currentVelocity;
    private Vector3 _targetVelocity;
    private Vector2 _inputDirection;
    [Header("Collision")]
    [SerializeField] private float _playerRadius = 0.4f;
    [SerializeField] private float _playerHeight = 1.8f;
    [SerializeField] private LayerMask _collisionMask;
    
    private Camera _camera;
    private Vector3 _dashDirection;
    public Vector3 DashDirection
    {
        get => _dashDirection;
    }

    public Transform Transform => _player.transform;
    public Vector3 TargetVelocity => _targetVelocity;
    public Vector3 CurrentVelocity => _currentVelocity;
    public Collider Collider => _collider;
    private void Start()
    {
        //_rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        
        _camera = Camera.main;
        PlayerSystems.instance.CanMove = true;
    }

    private void FixedUpdate()
    {
        if (PlayerSystems.instance.CanMove)
        {
            _inputDirection = _playerInput.actions["Move"].ReadValue<Vector2>();
            Vector3 moveDir = new Vector3(_inputDirection.x, 0f, _inputDirection.y).normalized;
            _targetVelocity = moveDir * characteristics.Current.speed;
            Debug.DrawLine(_rigidbody.position, _rigidbody.position + _currentVelocity, Color.red);
            _currentVelocity = Vector3.MoveTowards(_currentVelocity, _targetVelocity,
                (_targetVelocity.magnitude > 0 ? _acceleration : _deceleration) * Time.fixedDeltaTime);

            Vector3 _nextPos = _rigidbody.position + _currentVelocity * Time.fixedDeltaTime;
            Collider[] hits = Physics.OverlapCapsule(
                _nextPos + Vector3.up * 0.5f,
                _nextPos + Vector3.up * (_playerHeight - 0.5f),
                _playerRadius,
                _collisionMask
            );

            foreach (var hit in hits)
            {
                if (Physics.ComputePenetration(
                        _collider, _nextPos, transform.rotation,
                        hit, hit.transform.position, hit.transform.rotation,
                        out Vector3 dir, out float dist))
                {
                    _nextPos += dir * dist;
                }
            }

            _rigidbody.MovePosition(_nextPos);

            bool isMoving = _currentVelocity.sqrMagnitude > 0.001f;
            _animator.SetBool("IsWalking", isMoving);

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 mouseDirection =
                _camera.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y,
                    _camera.transform.position.z * -1)) - Vector3.one / 2;
            mouseDirection.Normalize();

            Vector2 direction = Vector2.zero;
            if (!mouseDirection.Equals(Vector2.zero) && !isMoving)
            {
                direction = mouseDirection;
                AnimatorRotateSprite(new Vector2(Mathf.Round(mouseDirection.x), Mathf.Round(mouseDirection.y)));
            }
            else if (isMoving)
            {
                direction = new Vector2(_currentVelocity.x, _currentVelocity.z);
            }

            _dashDirection = direction;
            AnimatorRotateSprite(direction);
        }
    }

    private void AnimatorRotateSprite(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        _animator.SetFloat("MoveX", direction.x);
        _animator.SetFloat("MoveY", direction.y);
        _animator.SetFloat("Angle", angle);
        _animator.SetFloat("X", direction.x);
        _animator.SetFloat("Y", direction.y);
        
    }
}
