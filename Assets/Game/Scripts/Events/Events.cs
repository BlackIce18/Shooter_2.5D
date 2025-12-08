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
    public EquipmentItemScriptableObject NewItemScriptableObject;
    
    public TryEquipEvent(EquipmentType type, EquipmentItemScriptableObject newItemScriptableObject)
    {
        this.type = type;
        this.NewItemScriptableObject = newItemScriptableObject;
    }
} 
public struct TryUnEquipEvent
{
    public EquipmentType type;
    public EquipmentItemScriptableObject itemScriptableObject;
    public InventoryItemUI inventoryItemUI;
    
    public TryUnEquipEvent(EquipmentType type, EquipmentItemScriptableObject itemScriptableObject, InventoryItemUI inventoryItemUI)
    {
        this.type = type;
        this.itemScriptableObject = itemScriptableObject;
        this.inventoryItemUI = inventoryItemUI;
    }
} 
public struct EquipEvent
{
    public EquipmentType type;
    public EquipmentItemScriptableObject ItemScriptableObject;

    public EquipEvent(EquipmentType type, EquipmentItemScriptableObject itemScriptableObject)
    {
        this.type = type;
        this.ItemScriptableObject = itemScriptableObject;
    }
}

public struct UnequipEvent
{
    public EquipmentType type;
    public EquipmentItemScriptableObject ItemScriptableObject;
    public UnequipEvent(EquipmentType type, EquipmentItemScriptableObject itemScriptableObject)
    {
        this.type = type;
        this.ItemScriptableObject = itemScriptableObject;
    }
}