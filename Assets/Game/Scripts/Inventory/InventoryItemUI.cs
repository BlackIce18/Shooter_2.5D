using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    public EquipmentItems data;
    public Image Icon;
    public Vector2Int OriginalSize => data != null ? data.Size : Vector2Int.one;
    public Vector2Int CurrentSize { get; private set; }
    public RectTransform Rect => (RectTransform)transform;
    private List<InventoryCell> _occupiedCells = new List<InventoryCell>();
    public List<InventoryCell> OccupiedCells => _occupiedCells;
    [HideInInspector] public InventoryGrid grid;
    [HideInInspector] public Vector2Int originalStartPos;
    private CanvasGroup _canvasGroup;
    public ItemTooltip tooltip;
    public ContextMenuUI contextMenuUI;
    
    private void Awake()
    {
        CurrentSize = OriginalSize;
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetData(EquipmentItems item)
    {
        data = item;
        CurrentSize = item.Size;
        if (Icon != null && item.Icon != null) Icon.sprite = item.Icon;
        
        Rect.sizeDelta = new Vector2(item.Size.x * grid.CellSize.x, item.Size.y * grid.CellSize.y);
    }
    public void OnDrag(PointerEventData eventData)
    {
        DragAndDropController.Instance?.OnDragUpdate(eventData.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        DragAndDropController.Instance?.StartDrag(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        DragAndDropController.Instance?.EndDrag();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            tooltip.Hide();
            contextMenuUI.gameObject.SetActive(true);
            contextMenuUI.BindAction(this);
            var floatingWindow = contextMenuUI.GetComponent<FloatingWindow>();
            floatingWindow.Show(eventData.position, data);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(contextMenuUI.gameObject.activeSelf) return;
        
        tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(contextMenuUI.gameObject.activeSelf) return;

        tooltip.Hide();
        tooltip.ClearFields();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(contextMenuUI.gameObject.activeSelf) return;

        tooltip.Show(eventData.position, data);
    }
}