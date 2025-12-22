using UnityEngine;

public struct DamageEvent
{
    public GameObject target;
    public float damage;
    public Vector3 hitPoint;

    public DamageEvent(GameObject target, float damage, Vector3 hitPoint)
    {
        this.target = target;
        this.damage = damage;
        this.hitPoint = hitPoint;
    }
}

public struct DeathEvent
{
    public GameObject target;

    public DeathEvent(GameObject target)
    {
        this.target = target;
    }
}

public struct SoundEvent
{
    public GameObject target;
    public AudioClip audioClip;

    public SoundEvent(GameObject target, AudioClip audioClip)
    {
        this.target = target;
        this.audioClip = audioClip;
    }
}
// Проверки можно ли экипировать
public struct TryEquipEvent
{
    public EquipmentType type;
    public EquipmentItemBaseScriptableObject NewItemBaseScriptableObject;
    
    public TryEquipEvent(EquipmentType type, EquipmentItemBaseScriptableObject newItemBaseScriptableObject)
    {
        this.type = type;
        this.NewItemBaseScriptableObject = newItemBaseScriptableObject;
    }
} 
public struct TryUnEquipEvent
{
    public EquipmentType type;
    public EquipmentItemBaseScriptableObject ItemBaseScriptableObject;
    public InventoryItemUIBase inventoryItemUI;
    
    public TryUnEquipEvent(EquipmentType type, EquipmentItemBaseScriptableObject itemBaseScriptableObject, InventoryItemUIBase inventoryItemUI)
    {
        this.type = type;
        this.ItemBaseScriptableObject = itemBaseScriptableObject;
        this.inventoryItemUI = inventoryItemUI;
    }
} 
public struct EquipEvent
{
    public EquipmentType type;
    public EquipmentItemBaseScriptableObject ItemBaseScriptableObject;

    public EquipEvent(EquipmentType type, EquipmentItemBaseScriptableObject itemBaseScriptableObject)
    {
        this.type = type;
        this.ItemBaseScriptableObject = itemBaseScriptableObject;
    }
}

public struct UnequipEvent
{
    public EquipmentType type;
    public EquipmentItemBaseScriptableObject ItemBaseScriptableObject;
    public UnequipEvent(EquipmentType type, EquipmentItemBaseScriptableObject itemBaseScriptableObject)
    {
        this.type = type;
        this.ItemBaseScriptableObject = itemBaseScriptableObject;
    }
}

public struct PickUpItemEvent
{
    public ItemBaseScriptableObject ItemBaseScriptableObject;
    public PickUpItemEvent(ItemBaseScriptableObject itemBase)
    {
        ItemBaseScriptableObject = itemBase;
    }
}