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
    public EquipmentItems newItem;
    
    public TryEquipEvent(EquipmentType type, EquipmentItems newItem)
    {
        this.type = type;
        this.newItem = newItem;
    }
} 
public struct TryUnEquipEvent
{
    public EquipmentType type;
    public EquipmentItems item;
    
    public TryUnEquipEvent(EquipmentType type, EquipmentItems item)
    {
        this.type = type;
        this.item = item;
    }
} 
public struct EquipEvent
{
    public EquipmentType type;
    public EquipmentItems item;

    public EquipEvent(EquipmentType type, EquipmentItems item)
    {
        this.type = type;
        this.item = item;
    }
}

public struct UnequipEvent
{
    public EquipmentType type;
    public EquipmentItems item;
    public UnequipEvent(EquipmentType type, EquipmentItems item)
    {
        this.type = type;
        this.item = item;
    }
}