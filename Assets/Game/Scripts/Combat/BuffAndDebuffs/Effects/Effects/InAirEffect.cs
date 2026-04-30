using UnityEngine;
[CreateAssetMenu(menuName = "Effects/InAir")]
public class InAirEffect : Effects
{
    [SerializeField] private float _tickInterval = 1f;
    private float _tickTimer;
    protected override void OnApply()
    {
        _tickTimer = _tickInterval;
        // Отключается возможность ходить
        // Отключается возможность использовать умения
        // Отключается возможность атаковать
        // Добавляется анимация в воздухе
        Debug.Log($"Apply {Owner.name}");
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (!IsActive) return;
        _tickTimer -= deltaTime;

        if (_tickTimer <= 0)
        {
            //EventBus.Publish(new DamageEvent(_target.gameObject, _damagePerSecond, Vector3.zero));
            _tickTimer = _tickInterval;
        }
        
        
    }

    protected override void OnEnd()
    {
        EventBus.Publish(new InAirOutEvent());
        Debug.Log($"Effect ended on {Owner.name}");
    }
}
