using System;
using UnityEngine;

public class HPMPUIUpdater : MonoBehaviour
{
    [SerializeField] private SliderValue _hpSlider;
    [SerializeField] private SliderValue _manaSlider;
    [SerializeField] private Characteristics _characteristics;

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerTakeDamageEvent>(PlayerTakeDamageEvent);
        EventBus.Subscribe<RecalculateCharacteristicsEvent>(RecalculateEvent);
        EventBus.Subscribe<UpdateLvlXpEvent>(UpdateLvlXpEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerTakeDamageEvent>(PlayerTakeDamageEvent);
        EventBus.Unsubscribe<RecalculateCharacteristicsEvent>(RecalculateEvent);
        EventBus.Unsubscribe<UpdateLvlXpEvent>(UpdateLvlXpEvent);
    }
    private void PlayerTakeDamageEvent(PlayerTakeDamageEvent e) {UpdateUI();}
    private void RecalculateEvent(RecalculateCharacteristicsEvent e) {UpdateUI();}
    private void UpdateLvlXpEvent(UpdateLvlXpEvent e) {UpdateUI();}
    private void UpdateUI()
    {
        Debug.Log(_characteristics.Current.health);
        _hpSlider.Slider.maxValue = _characteristics.Current.healthMax;
        _hpSlider.Slider.value = _characteristics.Current.health;
        _manaSlider.Slider.maxValue = _characteristics.Current.manaMax;
        _manaSlider.Slider.value = _characteristics.Current.mana;
    }
}
