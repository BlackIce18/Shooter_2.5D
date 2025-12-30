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
    [SerializeField] private Characteristics characteristics;
    [SerializeField] private List<Equipment> _currentEquipmentList;
    [SerializeField] private Equipment _equipmentTest;
    
    private void Awake()
    {
        UpdateCharacteristics();
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

        EquipItem(newEquipment.itemBaseScriptableObject);
        _currentEquipmentList.Add(newEquipment);
        
        EventBus.Publish(new EquipEvent(type, newItemBaseScriptableObject));
        EventBus.Publish(new SoundEvent(this.gameObject, newItemBaseScriptableObject.EquipSound));
    }

    public void TryUnEquipItem(EquipmentType type, EquipmentItemBaseScriptableObject itemBaseScriptableObject)
    {
        var sameSlot = _currentEquipmentList.Find(e => e.type == type);
        if (sameSlot != null)
        {
            if (sameSlot.itemBaseScriptableObject != null)
            {
                Debug.Log("Unequip");
                // Move to Inventory
                UnequipItem(sameSlot.itemBaseScriptableObject);
                EventBus.Publish(new UnequipEvent(sameSlot.type, sameSlot.itemBaseScriptableObject));
                EventBus.Publish(new SoundEvent(this.gameObject, sameSlot.itemBaseScriptableObject.UnEquipSound));
                sameSlot.itemBaseScriptableObject = null;
            }
            _currentEquipmentList.Remove(sameSlot);
        }
    }
    
    public void UpdateCharacteristics()
    {
        characteristics.ResetToBase();
        foreach (var eq in _currentEquipmentList)
        {
            if(eq.itemBaseScriptableObject == null) continue;
            EquipItem(eq.itemBaseScriptableObject);
        }
    }

    private void EquipItem(EquipmentItemBaseScriptableObject equipment)
    {
        characteristics.AddFlat(equipment.ItemStats.characteristicsData);
    }
    public void UnequipItem(EquipmentItemBaseScriptableObject equipment)
    {
        characteristics.Negate(equipment.ItemStats.characteristicsData);
    }
}
