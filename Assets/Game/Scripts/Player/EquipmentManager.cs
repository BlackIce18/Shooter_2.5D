using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public enum EquipmentSlot
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
    SecondaryWeapon
}
[Serializable]
public class Equipment
{
    public EquipmentSlot slot;
    public EquipmentItems item;

    public Equipment(EquipmentSlot newSlot, EquipmentItems newItem)
    {
        slot = newSlot;
        item = newItem;
    }
}
public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Characteristics characteristics;
    [SerializeField] private List<Equipment> _currentEquipmentList;

    public event Action<EquipmentSlot, EquipmentItems> OnEquip;
    public event Action<EquipmentSlot, EquipmentItems> OnUnequip;
    
    [SerializeField] private Equipment _equipmentTest;

    private void Start()
    {
        TryEquipItem(_equipmentTest.slot, _equipmentTest.item);
    }

    private void Awake()
    {
        UpdateCharacteristics();
    }

    public bool CheckCompatibility(EquipmentItems item)
    {
        return true;
    }
    public void TryEquipItem(EquipmentSlot slot, EquipmentItems newItem)
    {
        if (!CheckCompatibility(newItem))
        {
            // Lvl, класс, характеристики слабые
        }

        var sameSlot = _currentEquipmentList.Find(e => e.slot == slot);
        if (sameSlot != null)
        {
            if (sameSlot.item != null)
            {
                // Move to Inventory
                UnequipItem(sameSlot.item);
                OnUnequip?.Invoke(sameSlot.slot, sameSlot.item);
            }
            _currentEquipmentList.Remove(sameSlot);
        }

        Equipment newEquipment = new(slot, newItem);
        EquipItem(newEquipment.item);
        _currentEquipmentList.Add(newEquipment);
        OnEquip?.Invoke(slot, newItem);
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
