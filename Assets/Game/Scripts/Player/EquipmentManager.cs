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
    public EquipmentItems item;
    
    public Equipment(EquipmentType newType, EquipmentItems newItem)
    {
        type = newType;
        item = newItem;
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

    public bool CheckCompatibility(EquipmentItems item)
    {
        return true;
    }
    private void EquipEvent(TryEquipEvent tryEquipEvent)
    {
        TryEquipItem(tryEquipEvent.type, tryEquipEvent.newItem);
    }
    private void UnequipEvent(TryUnEquipEvent tryUnEquipEvent)
    {
        TryUnEquipItem(tryUnEquipEvent.type, tryUnEquipEvent.item);
    }
    public void TryEquipItem(EquipmentType type, EquipmentItems newItem)
    {
        if (!CheckCompatibility(newItem))
        {
            // Lvl, класс, характеристики слабые
        }

        TryUnEquipItem(type, newItem);

        Equipment newEquipment = new(type, newItem);

        EquipItem(newEquipment.item);
        _currentEquipmentList.Add(newEquipment);
        
        EventBus.Publish(new EquipEvent(type, newItem));
        EventBus.Publish(new SoundEvent(this.gameObject, newItem.EquipSound));
    }

    public void TryUnEquipItem(EquipmentType type, EquipmentItems item)
    {
        var sameSlot = _currentEquipmentList.Find(e => e.type == type);
        if (sameSlot != null)
        {
            if (sameSlot.item != null)
            {
                Debug.Log("Unequip");
                // Move to Inventory
                UnequipItem(sameSlot.item);
                EventBus.Publish(new UnequipEvent(sameSlot.type, sameSlot.item));
                EventBus.Publish(new SoundEvent(this.gameObject, item.UnEquipSound));
                sameSlot.item = null;
            }
            _currentEquipmentList.Remove(sameSlot);
        }
    }
    
    public void UpdateCharacteristics()
    {
        characteristics.ResetToBase();
        foreach (var eq in _currentEquipmentList)
        {
            if(eq.item == null) continue;
            EquipItem(eq.item);
        }
    }

    private void EquipItem(EquipmentItems equipment)
    {
        characteristics.Add(equipment.ItemStats.characteristicsData);
    }
    public void UnequipItem(EquipmentItems equipment)
    {
        characteristics.Remove(equipment.ItemStats.characteristicsData);
    }
}
