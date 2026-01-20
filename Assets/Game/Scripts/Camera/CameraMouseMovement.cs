using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[Serializable]
public enum CameraMovementDirection
{
    Top,
    Right,
    Bottom,
    Left
}

public class CameraMouseMovement : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private float _offset = 5f;
    [SerializeField] private float _borderSize = 5f;
    private void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        Vector2 mouse = Mouse.current.position.value;

        Vector3 dir = Vector3.zero;

        if (mouse.y >= Screen.height - _borderSize) dir += Vector3.forward;
        if (mouse.y <= _borderSize) dir += Vector3.back;
        if (mouse.x >= Screen.width - _borderSize) dir += Vector3.right;
        if (mouse.x <= _borderSize) dir += Vector3.left;

        if (dir != Vector3.zero)
        {
            var target = _cameraMovement.LookTarget.transform.position + _cameraMovement.Offset + dir.normalized * _offset;
            _cameraMovement.MoveTo(target, _cameraMovement.SmoothDelay);
        }
    }
    
}