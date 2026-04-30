using UnityEngine;

public class EnemyHitDetector : MonoBehaviour
{
    [SerializeField] private DamageReceiver _damageReceiver;
    [SerializeField] private HealthComponent _healthComponent;

    public DamageReceiver DamageReceiver => _damageReceiver;
    public HealthComponent HealthComponent => _healthComponent;
}
