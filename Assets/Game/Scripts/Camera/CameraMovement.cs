using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _lookTarget;
    [SerializeField] private float _smoothDelay;
    
    private Vector3 _offset;
    private void Start()
    {
        if(_mainCamera == null) _mainCamera = Camera.main;

        _offset = _mainCamera.transform.position - _lookTarget.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = _lookTarget.transform.position + _offset;
        _mainCamera.transform.position = Vector3.Lerp (_mainCamera.transform.position, targetCamPos, _smoothDelay * Time.deltaTime);
    }
}
