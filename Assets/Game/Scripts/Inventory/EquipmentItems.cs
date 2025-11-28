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
    [SerializeField] private EquipmentType equipmentType;
    [SerializeField] private ItemStats _itemStats;

    [SerializeField] private AudioClip _equipAudioClip;
    [SerializeField] private AudioClip _unEquipAudioClip;
    //[SerializeField] private List<RuneItem> _sockets = new();

    public ItemStats ItemStats => _itemStats;
    public AudioClip EquipSound => _equipAudioClip;
    public AudioClip UnEquipSound => _unEquipAudioClip;
    public override void Use(GameObject user)
    {
        EventBus.Publish(new TryEquipEvent(equipmentType, this));
    }
}
