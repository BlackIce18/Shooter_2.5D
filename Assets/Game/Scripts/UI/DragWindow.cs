using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragWindow : MonoBehaviour, IPointerDownHandler, IDragHandler
{ 
    [SerializeField] private RectTransform _rectWindow;
    [SerializeField] private Canvas _canvas;
    
    private Vector2 _offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectWindow,
            eventData.position,
            eventData.pressEventCamera,
            out _offset
        );
        
        Debug.Log("Pointer down");
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectWindow.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var pos
        );
        _rectWindow.localPosition = pos - _offset;
        Debug.Log("Pointer drag");
    }
}
