using UnityEngine;

public class EnemyCombatInterface : MonoBehaviour
{
    [SerializeField] private EffectsManager _effectsManager;

    public void AddEffect(Effects effect)
    {
        _effectsManager.AddEffect(effect);
    }
}
