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
    public EquipmentItemScriptableObject itemScriptableObject;
    
    public Equipment(EquipmentType newType, EquipmentItemScriptableObject newItemScriptableObject)
    {
        type = newType;
        itemScriptableObject = newItemScriptableObject;
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

    public bool CheckCompatibility(EquipmentItemScriptableObject itemScriptableObject)
    {
        return true;
    }
    private void EquipEvent(TryEquipEvent tryEquipEvent)
    {
        TryEquipItem(tryEquipEvent.type, tryEquipEvent.NewItemScriptableObject);
    }
    private void UnequipEvent(TryUnEquipEvent tryUnEquipEvent)
    {
        TryUnEquipItem(tryUnEquipEvent.type, tryUnEquipEvent.itemScriptableObject);
        /*tryUnEquipEvent.inventoryItemUI.grid.RemoveItem(tryUnEquipEvent.inventoryItemUI);
        tryUnEquipEvent.inventoryItemUI.gameObject.SetActive(false);*/
        //tryUnEquipEvent.inventoryItemUI.grid.TryAddItem(tryUnEquipEvent.inventoryItemUI.data, tryUnEquipEvent.inventoryItemUI.grid.ItemPrefab);
    }
    public void TryEquipItem(EquipmentType type, EquipmentItemScriptableObject newItemScriptableObject)
    {
        if (!CheckCompatibility(newItemScriptableObject))
        {
            // Lvl, класс, характеристики слабые
        }

        TryUnEquipItem(type, newItemScriptableObject);

        Equipment newEquipment = new(type, newItemScriptableObject);

        EquipItem(newEquipment.itemScriptableObject);
        _currentEquipmentList.Add(newEquipment);
        
        EventBus.Publish(new EquipEvent(type, newItemScriptableObject));
        EventBus.Publish(new SoundEvent(this.gameObject, newItemScriptableObject.EquipSound));
    }

    public void TryUnEquipItem(EquipmentType type, EquipmentItemScriptableObject itemScriptableObject)
    {
        var sameSlot = _currentEquipmentList.Find(e => e.type == type);
        if (sameSlot != null)
        {
            if (sameSlot.itemScriptableObject != null)
            {
                Debug.Log("Unequip");
                // Move to Inventory
                UnequipItem(sameSlot.itemScriptableObject);
                EventBus.Publish(new UnequipEvent(sameSlot.type, sameSlot.itemScriptableObject));
                EventBus.Publish(new SoundEvent(this.gameObject, itemScriptableObject.UnEquipSound));
                sameSlot.itemScriptableObject = null;
            }
            _currentEquipmentList.Remove(sameSlot);
        }
    }
    
    public void UpdateCharacteristics()
    {
        characteristics.ResetToBase();
        foreach (var eq in _currentEquipmentList)
        {
            if(eq.itemScriptableObject == null) continue;
            EquipItem(eq.itemScriptableObject);
        }
    }

    private void EquipItem(EquipmentItemScriptableObject equipment)
    {
        characteristics.Add(equipment.ItemStats.characteristicsData);
    }
    public void UnequipItem(EquipmentItemScriptableObject equipment)
    {
        characteristics.Remove(equipment.ItemStats.characteristicsData);
    }
}
