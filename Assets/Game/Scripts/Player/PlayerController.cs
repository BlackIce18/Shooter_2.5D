using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Animator _animator;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector2 inputDirection = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Vector2 mouseDirection = _camera.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y, _camera.transform.position.z * -1)) - Vector3.one / 2;
        mouseDirection.Normalize();
        
        if (inputDirection.Equals(Vector2.zero))
        {
            _animator.SetBool("IsWalking", false);
            if (!mouseDirection.Equals(Vector2.zero))
            {
                AnimatorRotateSprite(new Vector2(Mathf.Round(mouseDirection.x), Mathf.Round(mouseDirection.y)));
            }
        }
        else
        {
            _animator.SetBool("IsWalking", true);
        }

        AnimatorRotateSprite(inputDirection);
        
        _player.transform.Translate(new Vector3(inputDirection.x, 0, inputDirection.y) * _moveSpeed * Time.deltaTime, Space.World); 
    }

    private void AnimatorRotateSprite(Vector2 direction)
    {
        switch (direction)
        {
            case Vector2 v when v.Equals(Vector2.up):
                _animator.SetFloat("MouseX", 0f);
                _animator.SetFloat("MouseY", 1f);
                _animator.SetFloat("X", 0f);
                _animator.SetFloat("Y", 1f);
                break;
            case Vector2 v when v.Equals(Vector2.right):
                _animator.SetFloat("MouseX", 1f);
                _animator.SetFloat("MouseY", 0f);
                _animator.SetFloat("X", 1f);
                _animator.SetFloat("Y", 0f);
                break;
            case Vector2 v when v.Equals(Vector2.down):
                _animator.SetFloat("MouseX", 0f);
                _animator.SetFloat("MouseY", -1f);
                _animator.SetFloat("X", 0f);
                _animator.SetFloat("Y", -1f);
                break;
            case Vector2 v when v.Equals(Vector2.left):
                _animator.SetFloat("MouseX", -1f);
                _animator.SetFloat("MouseY", 0f);
                _animator.SetFloat("X", -1f);
                _animator.SetFloat("Y", 0f);
                break;
        }
    }
}
