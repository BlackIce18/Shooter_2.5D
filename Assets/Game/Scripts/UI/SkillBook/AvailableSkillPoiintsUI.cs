using System;
using TMPro;
using UnityEngine;

public class AvailableSkillPoiintsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        EventBus.Subscribe<UpdateAvailableSkillPoints>(UpdateText);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<UpdateAvailableSkillPoints>(UpdateText);
    }

    private void UpdateText(UpdateAvailableSkillPoints updateTextEvent)
    {
        _text.text = updateTextEvent.skillPoints.ToString();
    }
}

