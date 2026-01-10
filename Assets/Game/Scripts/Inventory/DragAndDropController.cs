using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropController : MonoBehaviour
{
    public static DragAndDropController Instance;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private InventoryGrid _grid;

    [SerializeField] private Image _ghostImage;
    [SerializeField] private CanvasGroup _ghostCanvasGroup;

    private InventoryItemUI _draggedItem;
    private InventoryGrid _originGrid;
    private Vector2Int _originStart;
    private bool _isDragging = false;
    private bool _rotatedDuringDrag = false;

    private void Awake()
    {
        Instance = this;
        _ghostImage.gameObject.SetActive(false);
    }
    public InventoryGrid GetGridUnderPointer(Vector2 screenPos)
    {
        var ev = new PointerEventData(EventSystem.current) { position = screenPos };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ev,results);
        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent(out InventoryGrid grid))
            {
                return grid;
            }

            var parentGrid = result.gameObject.GetComponentInParent<InventoryGrid>();
            if (parentGrid != null) return parentGrid;
        }
        return null;
    }
    public void OnDragUpdate(Vector2 screenPos)
    {
        Debug.Log("IsDragging:"+_isDragging);
        if (!_isDragging) return;
        
        _ghostImage.transform.position = screenPos;
        
        var newGrid = GetGridUnderPointer(screenPos);
        
        if (_grid != newGrid)
        {
            _grid?.ClearAllHighlights();
            _grid = newGrid;
        }

        if (_grid)
        {
            InventoryCell cell = _grid.GetCellUnderPointer(screenPos);
            if (cell == null)
            {
                _grid.ClearAllHighlights();
                return;
            }

            Vector2Int startPos = cell.position;
            startPos.x = Mathf.Clamp(startPos.x, 0, _grid.Size.x - _draggedItem.CurrentSize.x);
            startPos.y = Mathf.Clamp(startPos.y, 0, _grid.Size.y - _draggedItem.CurrentSize.y);

            _grid.HighlightArea(cell.position, _draggedItem.CurrentSize);
        }
    }

    public void StartDrag(InventoryItemUI item)
    {
        if (item == null) return;

        _draggedItem = item;
        _originGrid = item.grid;
        _originStart = item.originalStartPos;

        _originGrid?.RemoveItem(item);

        _ghostImage.sprite = item.data.Icon;
        _ghostImage.rectTransform.sizeDelta = item.Rect.sizeDelta;
        _ghostImage.gameObject.SetActive(true);
        if (_ghostCanvasGroup != null) _ghostCanvasGroup.alpha = 0.9f;

        _isDragging = true;
    }

    public void EndDrag()
    {
        if (!_isDragging || _draggedItem == null)
        {
            ClearDragState();
            return;
        }

        Vector2 mousePos = Input.mousePosition;
        if (_grid == null) _grid = _originGrid;

        InventoryCell cell = _grid.GetCellUnderPointer(mousePos);
        if (cell == null)
        {
            if (_grid.TryFindSpace(_draggedItem.CurrentSize, out var freePos))
            {
                _grid.PlaceItemAt(_draggedItem, freePos);
            }
            else
            {
                _originGrid.PlaceItemAt(_draggedItem, _originStart);
            }

            ClearDragState();
            return;
        }

        Vector2Int startPos = cell.position;
        startPos.x = Mathf.Clamp(startPos.x, 0, _grid.Size.x - _draggedItem.CurrentSize.x);
        startPos.y = Mathf.Clamp(startPos.y, 0, _grid.Size.y - _draggedItem.CurrentSize.y);

        if (_grid.CanPlaceAt(startPos, _draggedItem.CurrentSize))
        {
            _grid.PlaceItemAt(_draggedItem, startPos);
            ClearDragState();
            return;
        }

        var occupying = _grid.GetUniqueItemsInArea(startPos, _draggedItem.CurrentSize);
        if (occupying.Count == 1)
        {
            var other = occupying.First();
            if (_originGrid != null && _originGrid.CanPlaceAt(_originStart, other.CurrentSize))
            {
                _grid.RemoveItem(other);
                _originGrid.RemoveItem(_draggedItem);

                _grid.PlaceItemAt(_draggedItem, startPos);
                _originGrid.PlaceItemAt(other, _originStart);

                ClearDragState();
                return;
            }
        }

        _originGrid.PlaceItemAt(_draggedItem, _originStart);
        ClearDragState();
    }

    private void ClearDragState()
    {
        _ghostImage.gameObject.SetActive(false);
        _grid.ClearAllHighlights();
        _originGrid.ClearAllHighlights();
        _draggedItem = null;
        _originGrid = null;
        _isDragging = false;
    }
}