using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] private float _extraDelayAfterAnimation = 0.2f;
    private Mouse _mouse;
    private bool _isDashing = false;
    private float _damage = 1;

    private Vector3 mousePosition;
    private Vector2 mouseDirection;

    private void Start()
    {
        _camera = Camera.main;
        _targetPosition = _animator.transform.parent.localPosition;
        _mouse = Mouse.current;
        _hitbox.gameObject.SetActive(false);
        _hitbox.OnHit += HandleHit;
    }

    private void Update()
    {
        if (_mouse.leftButton.wasPressedThisFrame && _canAttack)
        {
            Attack();
            OnAttackAnimationEnd();
        }

        if (_isDashing)
        {
            if (_attackNumber == 0)
                MicroDash(_animator.transform, 0.08f);
            if (_attackNumber == 1)
                MicroDash(_animator.transform, 0.12f);
        }
    }

    private void Attack()
    {
        Vector3 mousePosition = _mouse.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        _attackNumber++;
        if (_attackNumber == 2) _attackNumber = 0;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Use the hit variable to determine what was clicked on.
        }
        //Debug.Log(_targetPosition);
    }

    private void MicroDash(Transform objectPosition, float distance)
    {
        mousePosition = _mouse.position.ReadValue();
        mouseDirection =
            _camera.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y,
                _camera.transform.position.z * -1)) - Vector3.one / 2;
        mouseDirection.Normalize();
        //_targetPosition = _animator.transform.parent.localPosition + (new Vector3(mouseDirection.x, 0, mouseDirection.y) * distance);
        _targetPosition = _animator.transform.parent.localPosition +
                          new Vector3(mouseDirection.x, 0, mouseDirection.y) * distance;
        //objectPosition.RotateAround(objectPosition.parent.transform.position, new Vector3(objectPosition.rotation.x, mouseDirection.x, objectPosition.rotation.z), 0.5f * Time.deltaTime);
        objectPosition.parent.localPosition =
            Vector3.SmoothDamp(objectPosition.parent.localPosition, _targetPosition, ref _velocity, 0.1f);
    }

    public void OnAttackAnimationEnd()
    {
        Debug.Log("Work");
        StartCoroutine(ResetAttackAfterDelay());
    }

    private IEnumerator ResetAttackAfterDelay()
    {
        _isDashing = true;
        _canAttack = false;
        _animator.SetTrigger("IsAttack");
        _animator.SetBool("IsAttacking", !_canAttack);
        _animator.SetFloat("AttackNumber", _attackNumber);
        _hitbox.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(_extraDelayAfterAnimation);
        
        _canAttack = true;
        _isDashing = false;
        _animator.ResetTrigger("IsAttack");
        _animator.SetBool("IsAttacking", !_canAttack);
        _hitbox.gameObject.SetActive(false);
    }

    private void HandleHit(IDamagable target)
    {
        target.TakeDamage(_damage);
    }

}
