using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : InventorySlot
{
    [SerializeField] private EquipmentSlot _equipmentSlot;
    [SerializeField] private EquipmentManager _equipmentManager;
    [SerializeField] private GameObject _prefab;
    private GameObject prefabInstance;
    private void Start()
    {
        if (IsEmpty)
        {
            Image.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _equipmentManager.OnEquipmentChanged += Equip;
    }

    private void OnDisable()
    {
        _equipmentManager.OnEquipmentChanged -= Equip;
    }

    private void Equip(EquipmentSlot slot, EquipmentItems item)
    {
        if (_equipmentSlot == slot)
        {
            Image.gameObject.SetActive(true);
            Image.sprite = item.Icon;
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        prefabInstance = Instantiate(_prefab, eventData.pointerCurrentRaycast.screenPosition, Quaternion.identity, this.transform.parent);
        prefabInstance.SetActive(true);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        prefabInstance.transform.position = eventData.pointerCurrentRaycast.screenPosition;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        //Destroy(prefabInstance);
    }
}