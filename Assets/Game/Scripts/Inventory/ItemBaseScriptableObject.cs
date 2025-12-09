using UnityEngine;
public enum ItemType
{
    Equipment,
    Usable,
    Trinket,
    Rune
}
public abstract class ItemBaseScriptableObject : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [Tooltip("1x1, 2x1 etc")][SerializeField] private Vector2Int _size;
    [SerializeField] private int _maxStack = 1;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _requiredLevel = 1;

    public virtual void OnPickUp() { }
    public virtual void OnDrop() { }

    public string Name => _name;
    public Sprite Icon => _icon;
    public Vector2Int Size => _size;
    public abstract void Use();
}
