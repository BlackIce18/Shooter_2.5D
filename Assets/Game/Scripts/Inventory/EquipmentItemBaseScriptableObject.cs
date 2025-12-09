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
public class EquipmentItemBaseScriptableObject : ItemBaseScriptableObject
{
    [SerializeField] private EquipmentType equipmentType;
    [SerializeField] private ItemStats _itemStats;

    [SerializeField] private AudioClip _equipAudioClip;
    [SerializeField] private AudioClip _unEquipAudioClip;
    //[SerializeField] private List<RuneItem> _sockets = new();
    
    public EquipmentType EquipmentType => equipmentType;
    public ItemStats ItemStats => _itemStats;
    public AudioClip EquipSound => _equipAudioClip;
    public AudioClip UnEquipSound => _unEquipAudioClip;
    public override void Use()
    {
        EventBus.Publish(new TryEquipEvent(equipmentType, this));
    }
}
