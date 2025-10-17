using UnityEngine;

public class TakeDamageHandler : MonoBehaviour
{
    private void OnEnable() => EventBus.Subscribe<DamageEvent>(OnTakeDamage);
    private void OnDisable() => EventBus.Unsubscribe<DamageEvent>(OnTakeDamage);

    private void OnTakeDamage(DamageEvent e)
    {
        if(e.target == null) return;
        
        var animator = GetComponent<Animator>();
        animator?.SetTrigger("WasAttacked");
    }
}
