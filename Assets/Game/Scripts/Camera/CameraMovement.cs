using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _lookTarget;
    [SerializeField] private float _smoothDelay;
    private Vector3 _velocity;
    private Vector3 _offset;

    public Vector3 Offset => _offset;
    public GameObject LookTarget => _lookTarget;
    public float SmoothDelay => _smoothDelay;
    public bool CanMove { get; set; }
    private void Start()
    {
        if(_mainCamera == null) _mainCamera = Camera.main;

        _offset = _mainCamera.transform.position - _lookTarget.transform.position;
        CanMove = true;
    }

    private void LateUpdate()
    {
        if(CanMove)
            Follow();
    }

    public void Follow(float smoothDelay = 0)
    {
        Vector3 targetCamPos = _lookTarget.transform.position + _offset;
        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, targetCamPos, smoothDelay);
    }
    public void Follow()
    {
        Vector3 targetCamPos = _lookTarget.transform.position + _offset;

        _mainCamera.transform.position =
            Vector3.SmoothDamp(
                _mainCamera.transform.position,
                targetCamPos,
                ref _velocity,
                _smoothDelay
            );
    }

    public void MoveTo(Vector3 targetCamPos, float smoothTime = 0.15f)
    {
        _mainCamera.transform.position =
            Vector3.SmoothDamp(
                _mainCamera.transform.position,
                targetCamPos,
                ref _velocity,
                smoothTime
            );
    }
    public void MoveToPlayerImmediately()
    {
        Vector3 targetCamPos = _lookTarget.transform.position + _offset;
        _mainCamera.transform.position = targetCamPos;
    }
    
    public void AddOffset(Vector3 delta)
    {
        LookTarget.transform.position += delta;
    }

    public void SnapToTarget()
    {
        _mainCamera.transform.position = LookTarget.transform.position + _offset;
    }
}
