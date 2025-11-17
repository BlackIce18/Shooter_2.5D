using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemUI : InventoryCell/*, IDragHandler, IBeginDragHandler, IEndDragHandler*/
{
    [SerializeField] private EquipmentSlot _equipmentSlot;
    [SerializeField] private EquipmentManager _equipmentManager;
    [SerializeField] private GameObject _prefab;
    private GameObject prefabInstance;

    public EquipmentItems data;
    public Vector2Int OriginalSize => data != null ? data.Size : Vector2Int.one;
    public Vector2Int CurrentSize { get; private set; }
    public List<InventoryCell> occupiedCells = new List<InventoryCell>();
    [HideInInspector] public InventoryGrid originalGrid;
    [HideInInspector] public Vector2Int originalStartPos;

    private void Awake()
    {
        CurrentSize = OriginalSize;
    }

    public void SetData(EquipmentItems data)
    {
        this.data = data;
        CurrentSize = data.Size;
        if (Image != null && data.Icon != null) Image.sprite = data.Icon;
    }
    /*private void Start()
    {
        if (IsEmpty)
        {
            Image.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _equipmentManager.OnEquip += Equip;
        _equipmentManager.OnUnequip += Unequip;
    }

    private void OnDisable()
    {
        _equipmentManager.OnEquip -= Equip;
        _equipmentManager.OnUnequip -= Unequip;
    }

    private void Equip(EquipmentSlot slot, EquipmentItems item)
    {
        if (_equipmentSlot == slot)
        {
            Image.gameObject.SetActive(true);
            Image.sprite = item.Icon;
        }
    }

    private void Unequip(EquipmentSlot slot, EquipmentItems item)
    {
        if (_equipmentSlot == slot)
        {
            Image.gameObject.SetActive(false);
            Image.sprite = null;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        prefabInstance = Instantiate(_prefab, eventData.pointerCurrentRaycast.screenPosition, Quaternion.identity, this.transform.parent);
        prefabInstance.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        prefabInstance.transform.position = eventData.pointerCurrentRaycast.screenPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      
        //Destroy(prefabInstance);
    }*/
}