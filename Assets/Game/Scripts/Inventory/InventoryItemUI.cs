using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    public EquipmentItemScriptableObject data;
    public Image Icon;
    public Vector2Int OriginalSize => data != null ? data.Size : Vector2Int.one;
    public Vector2Int CurrentSize { get; private set; }
    public RectTransform Rect => (RectTransform)transform;
    private List<InventoryCell> _occupiedCells = new List<InventoryCell>();
    public List<InventoryCell> OccupiedCells => _occupiedCells;
    /*[HideInInspector]*/ public InventoryGrid grid;
    [HideInInspector] public Vector2Int originalStartPos;
    [HideInInspector] public CanvasGroup canvasGroup;
    public ItemTooltip tooltip;
    [SerializeField] private ContextMenuUI _ContextMenuUI;
    public ContextMenuUI ContextMenuUI { get => _ContextMenuUI; set => _ContextMenuUI = value; }
    private void Awake()
    {
        CurrentSize = OriginalSize;
    }

    protected void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetData(EquipmentItemScriptableObject itemScriptableObject)
    {
        data = itemScriptableObject;
        CurrentSize = itemScriptableObject.Size;
        if (Icon != null && itemScriptableObject.Icon != null) Icon.sprite = itemScriptableObject.Icon;
        
        Rect.sizeDelta = new Vector2(itemScriptableObject.Size.x * grid.CellSize.x, itemScriptableObject.Size.y * grid.CellSize.y);
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        DragAndDropController.Instance?.OnDragUpdate(eventData.position);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        DragAndDropController.Instance?.StartDrag(this);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        DragAndDropController.Instance?.EndDrag();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            tooltip.Hide();
            ContextMenuUI.gameObject.SetActive(true);
            ContextMenuUI.EquipAction(this);
            var floatingWindow = ContextMenuUI.GetComponent<FloatingWindow>();
            floatingWindow.Show(eventData.position, data);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ContextMenuUI.gameObject.activeSelf) return;
        
        tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(ContextMenuUI.gameObject.activeSelf) return;

        tooltip.Hide();
        tooltip.ClearFields();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(ContextMenuUI.gameObject.activeSelf) return;

        tooltip.Show(eventData.position, data);
    }
}