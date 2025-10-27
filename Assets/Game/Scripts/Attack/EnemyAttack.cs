using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private int _attackNumber = 0;
    [SerializeField] private AttackHitbox _hitbox;
    //private bool _canAttack = true;
    [SerializeField] private float _timeBetweenAttack = 0.2f;
    private float _currentTimeBetweenAttack = 0;
    [SerializeField] private float _damage = 1;
    [SerializeField] private EnemyFSM _enemyFsm;
    [SerializeField] private float _attackDistance = 1f;
    private void Start()
    {
        _hitbox.gameObject.SetActive(false);
        _currentTimeBetweenAttack = 0;
        _hitbox.OnHit += HandleHit;
    }
    private void Update()
    {
        if (_currentTimeBetweenAttack <= 0)
        {
            _enemyFsm.inAttackRange = Vector3.Distance(_enemyFsm.Player.position, transform.position) <= 0.8f;
            
            if (_enemyFsm.inAttackRange && _enemyFsm.canAttack)
            {
                Attack();
                _enemyFsm.canAttack = false;
                _animator.SetTrigger("IsAttack");
                _animator.SetBool("IsAttacking", true);
                _animator.SetFloat("AttackNumber", _attackNumber);
                _hitbox.gameObject.SetActive(true);
                StartCoroutine(ResetAttack());
            }
        }
        else
        {
            _currentTimeBetweenAttack -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        _attackNumber++;
        if (_attackNumber == 2) _attackNumber = 0;
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.25f);
        _animator.SetBool("IsAttacking", false); 
        _hitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(_timeBetweenAttack - 0.25f);
        _enemyFsm.canAttack = true;
        _animator.ResetTrigger("IsAttack");

        _currentTimeBetweenAttack = _timeBetweenAttack;
    }
    
    private void HandleHit(HealthHandler target)
    {
        EventBus.Publish(new DamageEvent(target.gameObject, _damage, Vector3.zero));
    }
}
