using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InventoryItemUIBase : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image Icon;
    public Vector2Int CurrentSize { get; set; }
    public RectTransform Rect => (RectTransform)transform;
    private List<InventoryCell> _occupiedCells = new List<InventoryCell>();
    public List<InventoryCell> OccupiedCells => _occupiedCells;
    /*[HideInInspector]*/ public InventoryGrid grid;
    [HideInInspector] public Vector2Int originalStartPos;
    [HideInInspector] public CanvasGroup canvasGroup;
    public ItemTooltip tooltip;
    [SerializeField] private ContextMenuUI _ContextMenuUI;
    public ContextMenuUI ContextMenuUI { get => _ContextMenuUI; set => _ContextMenuUI = value; }
    public abstract void OnDrag(PointerEventData eventData);
    public abstract void OnBeginDrag(PointerEventData eventData);
    public abstract void OnEndDrag(PointerEventData eventData);
}
public class InventoryItemUI : InventoryItemUIBase, IPointerClickHandler, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    public ItemBaseScriptableObject data;
    public Vector2Int OriginalSize => data != null ? data.Size : Vector2Int.one;
 
    private void Awake()
    {
        CurrentSize = OriginalSize;
    }

    protected void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetData(ItemBaseScriptableObject itemBaseScriptableObject)
    {
        data = itemBaseScriptableObject;
        CurrentSize = itemBaseScriptableObject.Size;
        if (Icon != null && itemBaseScriptableObject.Icon != null) Icon.sprite = itemBaseScriptableObject.Icon;
        
        Rect.sizeDelta = new Vector2(itemBaseScriptableObject.Size.x * grid.CellSize.x, itemBaseScriptableObject.Size.y * grid.CellSize.y);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        DragAndDropController.Instance?.OnDragUpdate(eventData.position);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        DragAndDropController.Instance?.StartDrag(this);
    }

    public override void OnEndDrag(PointerEventData eventData)
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
            floatingWindow.Show();
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