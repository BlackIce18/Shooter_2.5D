using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Animator _animator;
    private int _attackNumber = 0;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _targetPosition;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private AttackHitbox _hitbox;
    private bool _canAttack = true;
    [SerializeField] private float _timeBetweenAttack = 0.2f;
    private float _currentTimeBetweenAttack = 0;
    [SerializeField] private float _dashResetTime = 0.2f;
    [SerializeField] private float _dashDistance;
    private Mouse _mouse;
    private bool _isDashing = false;
    //[SerializeField] private float _damage = 1;
    [SerializeField] private Characteristics _characteristics;

    private Vector3 mousePosition;
    private Vector2 mouseDirection;
    private void Start()
    {
        _camera = Camera.main;
        _targetPosition = _animator.transform.parent.localPosition;
        _mouse = Mouse.current;
        _hitbox.gameObject.SetActive(false);
        _currentTimeBetweenAttack = 0;
        _hitbox.OnHit += HandleHit;
    }
    private void Update()
    {
        if (_currentTimeBetweenAttack <= 0)
        {
            if (_mouse.leftButton.wasPressedThisFrame && _canAttack)
            {
                Attack();
                _canAttack = false;
                _animator.SetTrigger("IsAttack");
                _animator.SetBool("IsAttacking", true);
                _animator.SetFloat("AttackNumber", _attackNumber);
                _hitbox.gameObject.SetActive(true);
                StartCoroutine(ResetDashAfterDelay());
                StartCoroutine(ResetAttack());
            }
            if (_isDashing)
            {
                if (_attackNumber == 0)
                    MicroDash(_animator.transform, _dashDistance / 2);
                if (_attackNumber == 1)
                    MicroDash(_animator.transform, _dashDistance);
            }
        }
        else
        {
           
            _currentTimeBetweenAttack -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        Vector3 mousePosition = _mouse.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        _attackNumber++;
        if (_attackNumber == 3) _attackNumber = 0;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Use the hit variable to determine what was clicked on.
        }
        
        //Debug.Log(_targetPosition);
    }

    private void MicroDash(Transform objectPosition, float distance)
    {
        mousePosition = _mouse.position.ReadValue();
        mouseDirection = _camera.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y, _camera.transform.position.z * -1)) - Vector3.one / 2;
        mouseDirection.Normalize();
        //_targetPosition = _animator.transform.parent.localPosition + (new Vector3(mouseDirection.x, 0, mouseDirection.y) * distance);
        _targetPosition = _animator.transform.parent.localPosition + new Vector3(mouseDirection.x, 0, mouseDirection.y) * distance;
        //objectPosition.RotateAround(objectPosition.parent.transform.position, new Vector3(objectPosition.rotation.x, mouseDirection.x, objectPosition.rotation.z), 0.5f * Time.deltaTime);
        objectPosition.parent.localPosition = Vector3.SmoothDamp(objectPosition.parent.localPosition, _targetPosition, ref _velocity, 0.1f);
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.25f);
        _animator.SetBool("IsAttacking", false); 
        _hitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(_timeBetweenAttack - 0.25f);
        _canAttack = true;
        _animator.ResetTrigger("IsAttack");

        _currentTimeBetweenAttack = _timeBetweenAttack;
    }

    private IEnumerator ResetDashAfterDelay()
    {
        _isDashing = true;
        yield return new WaitForSeconds(_dashResetTime);
        _isDashing = false;
    }
    private void HandleHit(HealthHandler target) 
    {
        Debug.Log(_characteristics.Current.attack);
        EventBus.Publish(new DamageEvent(target.gameObject, _characteristics.Current.attack, Vector3.zero));
    }

}
