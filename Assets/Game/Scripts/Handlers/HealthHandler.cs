using UnityEngine;
[RequireComponent(typeof(AudioData))]
public class HealthHandler : MonoBehaviour
{
    [SerializeField] private float _health = 30;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioData _audioData;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                EventBus.Publish(new DeathEvent(gameObject));
                EventBus.Publish(new SoundEvent(gameObject, _audioData.soundSet.deathSound));
            }
        }
    }
    
    private void OnEnable() => EventBus.Subscribe<DamageEvent>(OnTakeDamage);
    private void OnDisable() => EventBus.Unsubscribe<DamageEvent>(OnTakeDamage);

    public void OnTakeDamage(DamageEvent e)
    {
        if(e.target != gameObject) return;
        Health -= e.damage;
        var animator = GetComponent<Animator>();
        animator?.SetTrigger("WasAttacked");
        EventBus.Publish(new SoundEvent(gameObject, _audioData.soundSet.hitSound));
        Debug.Log($"{Health} осталось хп");
    }
}
