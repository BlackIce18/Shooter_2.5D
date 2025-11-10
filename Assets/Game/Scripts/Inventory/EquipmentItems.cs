using System;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    head,
    body,
    pants,
    belt,
    gloves,
    ring,
    amulet
}
[Serializable]
public struct ItemStats
{
    public float attack;
    public float attackRate;
    public float defence;
    public float speed;
    public float critChance;
    public float critPercentage;
    public float defenceResistance;
    public float fireResistance;
    public float critResistance;
}
[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentItems : Item
{
    [SerializeField] private EquipmentSlot _equipmentSlot;

    [SerializeField] private List<ItemStats> _itemStatsList;
    //[SerializeField] private List<RuneItem> _sockets = new();
    public override void Use(GameObject user)
    {
        
    }
}
