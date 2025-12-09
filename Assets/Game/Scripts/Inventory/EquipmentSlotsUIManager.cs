    using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

[Serializable]
public class EquipmentUI
{
    public EquipmentType equipmentType;
    public InventoryItemUI inventoryItemUI;
    
    public EquipmentUI(EquipmentType newEquipmentType, InventoryItemUI newInventoryItemUI)
    {
        equipmentType = newEquipmentType;
        inventoryItemUI = newInventoryItemUI;
    }
}
public class EquipmentSlotsUIManager : MonoBehaviour
{
    [SerializeField] private List<EquipmentUI> _slots;
    [SerializeField] private EquipmentManager _equipmentManager;
    
    private void OnEnable()
    {
        EventBus.Subscribe<EquipEvent>(Equip);
        EventBus.Subscribe<UnequipEvent>(Unequip);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<EquipEvent>(Equip);
        EventBus.Unsubscribe<UnequipEvent>(Unequip);
    }

    public void Equip(EquipEvent equipEvent)
    {
        EquipmentUI equipment = _slots.FirstOrDefault(s=> s.equipmentType == equipEvent.type);
        if(equipment == null) return;
        Debug.Log("EquipmentSlotsUIMAnager accept equip event");

        equipment.inventoryItemUI.data = equipEvent.ItemBaseScriptableObject;
        equipment.inventoryItemUI.Icon.sprite = equipEvent.ItemBaseScriptableObject.Icon;
        equipment.inventoryItemUI.Icon.gameObject.SetActive(true);
    }

    public void Unequip(UnequipEvent unequipEvent)
    {
        EquipmentUI equipment = _slots.FirstOrDefault(s=> s.equipmentType == unequipEvent.type);
        if(equipment == null) return;
        Debug.Log("EquipmentSlotsUIMAnager accept unequip event");
        equipment.inventoryItemUI.grid.TryAddItem(equipment.inventoryItemUI.data, equipment.inventoryItemUI.grid.ItemPrefab);
        equipment.inventoryItemUI.data = null;
        equipment.inventoryItemUI.Icon.sprite = null;
        equipment.inventoryItemUI.Icon.gameObject.SetActive(false);
    }
}
