using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Если модифицировать, то изменять switch,case EquipmentSlotsUIManager
[Serializable]
public enum EquipmentType
{
    head,
    body,
    pants,
    belt,
    gloves,
    boots,
    ring,
    amulet,
    PrimaryWeapon,
    SecondaryWeapon,
    none
}
[Serializable]
public class Equipment
{
    public EquipmentType type;
    public EquipmentItemBaseScriptableObject itemBaseScriptableObject;
    
    public Equipment(EquipmentType newType, EquipmentItemBaseScriptableObject newItemBaseScriptableObject)
    {
        type = newType;
        itemBaseScriptableObject = newItemBaseScriptableObject;
    }
}
public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private List<Equipment> _currentEquipmentList;
    
    private void Awake()
    {
        RegisterModifiers();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<TryEquipEvent>(EquipEvent);
        EventBus.Subscribe<TryUnEquipEvent>(UnequipEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<TryEquipEvent>(EquipEvent);
        EventBus.Unsubscribe<TryUnEquipEvent>(UnequipEvent);
    }

    public bool CheckCompatibility(EquipmentItemBaseScriptableObject itemBaseScriptableObject)
    {
        return true;
    }
    private void EquipEvent(TryEquipEvent tryEquipEvent)
    {
        TryEquipItem(tryEquipEvent.type, tryEquipEvent.NewItemBaseScriptableObject);
    }
    private void UnequipEvent(TryUnEquipEvent tryUnEquipEvent)
    {
        TryUnEquipItem(tryUnEquipEvent.type, tryUnEquipEvent.ItemBaseScriptableObject);
    }
    public void TryEquipItem(EquipmentType type, EquipmentItemBaseScriptableObject newItemBaseScriptableObject)
    {
        if (!CheckCompatibility(newItemBaseScriptableObject))
        {
            // Lvl, класс, характеристики слабые
        }

        TryUnEquipItem(type, newItemBaseScriptableObject);

        Equipment newEquipment = new(type, newItemBaseScriptableObject);
        _currentEquipmentList.Add(newEquipment);

        //EquipItem(newEquipment.itemBaseScriptableObject);
        EventBus.Publish(new EquipEvent(type, newItemBaseScriptableObject));
        EventBus.Publish(new SoundEvent(this.gameObject, newItemBaseScriptableObject.EquipSound));
        RegisterModifiers();
        EventBus.Publish(new RecalculateCharacteristicsEvent());
    }

    public void TryUnEquipItem(EquipmentType type, EquipmentItemBaseScriptableObject itemBaseScriptableObject)
    {
        var sameSlot = _currentEquipmentList.Find(e => e.type == type);
        if (sameSlot == null) return;

        var item = sameSlot.itemBaseScriptableObject;
        _currentEquipmentList.Remove(sameSlot);
        
        RegisterModifiers();
        if (item == null) return;
        EventBus.Publish(new RecalculateCharacteristicsEvent());
        EventBus.Publish(new UnequipEvent(sameSlot.type, sameSlot.itemBaseScriptableObject));
        EventBus.Publish(new SoundEvent(this.gameObject, sameSlot.itemBaseScriptableObject.UnEquipSound));
    }
    
    public void RegisterModifiers()
    {
        foreach (var eq in _currentEquipmentList)
        {
            if(eq.itemBaseScriptableObject == null) continue;
            
            _characteristics.RegisterFlat(eq.itemBaseScriptableObject.ItemStats.flatCharacteristics);
            _characteristics.RegisterPercent(eq.itemBaseScriptableObject.ItemStats.percentCharacteristics);
        }
    }
}
