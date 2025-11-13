using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private bool _empty = true;

    public bool IsEmpty => _empty;
    public Image Image => _image;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("123");
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        Debug.Log("111");
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("321");
    }

}
