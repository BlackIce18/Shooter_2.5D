using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;
    
    private void Update()
    {
        Vector2 inputDirection = _playerInput.actions["Move"].ReadValue<Vector2>();
        if (inputDirection.Equals(Vector2.zero))
        {
            _animator.SetBool("IsWalking", false);
        }
        else
        {
            _animator.SetBool("IsWalking", true);
        }
        Debug.Log(inputDirection);
        switch (inputDirection)
        {
            case Vector2 v when v.Equals(Vector2.up):
                _animator.SetFloat("X", 0f);
                _animator.SetFloat("Y", 1f);
                break;
            case Vector2 v when v.Equals(Vector2.right):
                _animator.SetFloat("X", 1f);
                _animator.SetFloat("Y", 0f);
                break;
            case Vector2 v when v.Equals(Vector2.down):
                _animator.SetFloat("X", 0f);
                _animator.SetFloat("Y", -1f);
                break;
            case Vector2 v when v.Equals(Vector2.left):
                _animator.SetFloat("X", -1f);
                _animator.SetFloat("Y", 0f);
                break;
        }
        _player.transform.Translate(new Vector3(inputDirection.x, 0, inputDirection.y) * _moveSpeed * Time.deltaTime, Space.World); 
    }
}
