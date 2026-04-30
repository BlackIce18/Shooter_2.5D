using System;
using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;
    [SerializeField] private LayerMask collisionMask;

    [SerializeField] private Action OnDashStart;
    [SerializeField] private Action OnDashEnd;
    [SerializeField] private Action<Collider> OnDashHit;

    [SerializeField] private Rigidbody _rigidbody;
    private Coroutine _dashRoutine;
    private bool _isDashing = false;
    private bool _canDash = true;
    public bool IsDashing => _isDashing;
    public bool CanDash => _canDash;

    public void StartDash(Vector3 direction)
    {
        if(_isDashing || !_canDash) return;
        _dashRoutine = StartCoroutine(DashRoutine(direction.normalized));
    }

    private IEnumerator DashRoutine(Vector3 direction)
    {
        _isDashing = true;
        _canDash = false;
        OnDashStart?.Invoke();

        float time = 0f;
        
        while (time < dashDuration)
        {
            time += Time.fixedDeltaTime;
            Vector3 nextPos = _rigidbody.transform.position + new Vector3(direction.x, 0, direction.y) * (_dashSpeed * Time.fixedDeltaTime);

            if (Physics.CapsuleCast(_rigidbody.transform.position + Vector3.up * 0.3f, _rigidbody.transform.position + Vector3.up * 1f, 0.3f,
                    direction, out RaycastHit hit, _dashSpeed * Time.fixedDeltaTime, collisionMask))
            {
                OnDashHit?.Invoke(hit.collider);
                break;
            }
            
            if (_rigidbody != null && !_rigidbody.isKinematic)
            {
                _rigidbody.MovePosition(nextPos);
            }
            else
            {
                _rigidbody.transform.position = new Vector3(nextPos.x, _rigidbody.transform.position.y, nextPos.z);
            }
            
            yield return new WaitForFixedUpdate();
        }
        _isDashing = false;
        OnDashEnd?.Invoke();
        
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

}
