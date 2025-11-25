using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[Serializable]
public struct ItemTooltipDataStruct
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;
}

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform  _rect;
    [SerializeField] private Vector2 _tooltipOffset;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private ItemTooltipDataStruct itemTooltipDataStruct; 
    
    private void Start()
    {
        _canvas = gameObject.GetComponentInParent<Canvas>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        /*if (_rect == null) return;
        
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
        anchoredPos += new Vector2(_rect.rect.width / 2f + _tooltipOffset.x, -_rect.rect.height / 2f - _tooltipOffset.y);
        _rect.anchoredPosition = anchoredPos;
        
        _rect.gameObject.SetActive(true);*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //_rect.gameObject.SetActive(false);
    }

    public void Show(Vector3 position, EquipmentItems data)
    {
        if (_rect == null) return;
        
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
        anchoredPos += new Vector2(_rect.rect.width / 2f + _tooltipOffset.x, -_rect.rect.height / 2f - _tooltipOffset.y);
        _rect.anchoredPosition = anchoredPos;
        
        _rect.gameObject.SetActive(true);

        itemTooltipDataStruct.name.text = data.name;
        itemTooltipDataStruct.description.text = data.Size.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ClearFields()
    {
        itemTooltipDataStruct.name.text = "";
        itemTooltipDataStruct.description.text = "";
    }
}
