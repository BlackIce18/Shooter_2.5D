using System;
using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private LayerMask collisionMask;

    [SerializeField] private Action OnDashStart;
    [SerializeField] private Action OnDashEnd;
    [SerializeField] private Action<Collider> OnDashHit;

    [SerializeField] private Rigidbody _rigidbody;
    private Coroutine _dashRoutine;
    private bool _isDashing = false;
    public bool IsDashing => _isDashing;

    public void StartDash(Vector3 direction)
    {
        if(_isDashing) return;
        _dashRoutine = StartCoroutine(DashRoutine(direction.normalized));
    }

    private IEnumerator DashRoutine(Vector3 direction)
    {
        _isDashing = true;
        OnDashStart?.Invoke();

        float time = 0f;
        
        while (time < dashDuration)
        {
            time += Time.fixedDeltaTime;
            Vector3 nextPos = _rigidbody.transform.position + direction * (_dashSpeed * Time.fixedDeltaTime);

            if (Physics.CapsuleCast(_rigidbody.transform.position + Vector3.up * 0.3f, _rigidbody.transform.position + Vector3.up * 1f, 0.3f,
                    direction, out RaycastHit hit, _dashSpeed * Time.fixedDeltaTime, collisionMask))
            {
                OnDashHit?.Invoke(hit.collider);
                break;
            }


            if (_rigidbody != null && _rigidbody.isKinematic)
            {
                _rigidbody.MovePosition(nextPos);
            }
            
            yield return new WaitForFixedUpdate();
        }
        
        _isDashing = false;
        OnDashEnd?.Invoke();
    }

}
