using UnityEngine;
using System.Collections;

public class WarriorEnemy : EnemyFSM
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AttackHitbox _hitbox;
    private int _attackNumber = 0;
    public override bool InAttackRange => Vector3.Distance(transform.position, PlayerPosition) < characteristics.Current.attackDistance;
    protected override void Awake()
    {
        base.Awake();
        
        
        Fsm.ChangeState(new EnemyIdleState());
    }
    private void Start()
    {
        _hitbox.gameObject.SetActive(false);
        _hitbox.OnHit += HandleHit;
    }
    public override void MoveTo(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, 2f * Time.deltaTime);
    }
    private void Update()
    {
        base.Update();
        if (InAttackRange && CanAttack)
        {
            Attack();
        }
    }

    protected override void PerformAttack()
    {
        _attackNumber++;
        if (_attackNumber == 2) _attackNumber = 0;
        
        _animator.SetTrigger("IsAttack");
        _animator.SetBool("IsAttacking", true);
        _animator.SetFloat("AttackNumber", _attackNumber);

        StartCoroutine(AttackSequence());
    }
    
    private IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(characteristics.Current.attackDelay);
        _hitbox.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.2f);
        _hitbox.gameObject.SetActive(false);

        _animator.SetBool("IsAttacking", false);
    }
    
    private void HandleHit(HealthHandler target)
    {
        EventBus.Publish(new DamageEvent(target.gameObject, characteristics.Current.attack, Vector3.zero));
    }
}
