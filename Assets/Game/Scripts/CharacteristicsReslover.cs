using System;
using UnityEngine;

public class CharacteristicsReslover : MonoBehaviour
{
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private BuffDebuffController _buffDebuffController;
    [SerializeField] private EquipmentManager _equipmentManager;

    private void OnEnable()
    {
        EventBus.Subscribe<RecalculateCharacteristicsEvent>(RecalculateEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<RecalculateCharacteristicsEvent>(RecalculateEvent);
    }

    public void RecalculateEvent(RecalculateCharacteristicsEvent e) {RecalculateAll();}
    
    public void RecalculateAll()
    {
        _characteristics.ClearModifiers();
        
        _equipmentManager.UpdateCharacteristics();
        _buffDebuffController.RecalculateStats();
        
        _characteristics.Recalculate();
    }
}
