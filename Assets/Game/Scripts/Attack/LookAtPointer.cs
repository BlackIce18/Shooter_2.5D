using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtPointer : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private LayerMask _groundMask;
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _groundMask))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0;
            
            if (dir.sqrMagnitude > 0.0001f)
            {
                Quaternion lookRot = Quaternion.LookRotation(dir);
                Quaternion offset = Quaternion.Euler(90f, 0f, 0f);
                transform.rotation = lookRot * offset;
            }
        }
    }
}
