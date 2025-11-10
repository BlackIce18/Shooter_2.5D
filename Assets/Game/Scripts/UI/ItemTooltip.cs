using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform  Tooltip;
    [SerializeField] private Vector2 _tooltipOffset;
    [SerializeField] private PlayerInput _playerInput;
    private Camera _camera;
    [SerializeField] private Canvas _canvas;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Tooltip == null) return;
        
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 anchoredPos;
        
        RectTransform canvasRect = _canvas.transform as RectTransform;
        if (canvasRect == null)
        {
            Debug.LogError("Canvas не является RectTransform!");
            return;
        }
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mousePosition,
            null,
            out anchoredPos
        );
        anchoredPos += new Vector2(Tooltip.rect.width / 2f + _tooltipOffset.x, -Tooltip.rect.height / 2f - _tooltipOffset.y);
        Tooltip.anchoredPosition = anchoredPos;
        
        Tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.gameObject.SetActive(false);
    }
}
