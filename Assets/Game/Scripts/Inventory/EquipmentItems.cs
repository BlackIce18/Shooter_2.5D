using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemStats
{
    public CharacteristicsData characteristicsData;
    public int requiredClass;
}
[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentItems : Item
{
    [SerializeField] private EquipmentSlot _equipmentSlot;

    [SerializeField] private ItemStats _itemStats;
    //[SerializeField] private List<RuneItem> _sockets = new();

    public ItemStats ItemStats => _itemStats;
    public override void Use(GameObject user)
    {
        user.GetComponent<EquipmentManager>()?.TryEquipItem(_equipmentSlot, this);
    }
}
