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

    public event Action OnEquipmentChanged;
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
            UnequipItem(sameSlot.item);
            _currentEquipmentList.Remove(sameSlot);
        }

        Equipment newEquipment = new(slot, newItem);
        EquipItem(newEquipment.item);
        _currentEquipmentList.Add(newEquipment);
        OnEquipmentChanged?.Invoke();
    }
    
    public void UpdateCharacteristics()
    {
        characteristics.ResetToBase();
        foreach (var eq in _currentEquipmentList)
        {
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
