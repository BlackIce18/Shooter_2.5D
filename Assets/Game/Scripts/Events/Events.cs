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

public struct PlayerTakeDamageEvent
{
    public GameObject target;
    public float damage;
    public Vector3 hitPoint;

    public PlayerTakeDamageEvent(GameObject target, float damage, Vector3 hitPoint)
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
public struct PitchedSoundEvent
{
    public GameObject target;
    public AudioClip audioClip;
    public Vector2 randomDiaposon;
    public PitchedSoundEvent(GameObject target, AudioClip audioClip, Vector2 randomDiaposon)
    {
        this.target = target;
        this.audioClip = audioClip;
        this.randomDiaposon = randomDiaposon;
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

public struct AddXpEvent
{
    public int xp;
    public AddXpEvent(int xp)
    {
        this.xp = xp;
    }
}
public struct SubXpEvent
{
    public int xp;
    public SubXpEvent(int xp)
    {
        this.xp = xp;
    }
}

public struct LvlUpEvent
{
    public int lvl;
    public LvlUpEvent(int lvl)
    {
        this.lvl = lvl;
    }
}

public struct LvlDownEvent
{
    public int lvl;
    public LvlDownEvent(int lvl)
    {
        this.lvl = lvl;
    }
}

public struct UpdateLvlXpEvent
{
    public int lvl;
    public int xp;

    public UpdateLvlXpEvent(int lvl, int xp)
    {
        this.lvl = lvl;
        this.xp = xp;
    }
}

public struct RecalculateCharacteristicsEvent{ }