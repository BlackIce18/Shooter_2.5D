using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    public Vector2Int Size => _size;

    [SerializeField] private List<InventoryCell> _cellsList = new();
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    private InventoryCell[,] _cells;
    private Dictionary<Vector2Int, InventoryCell> _map = new();
    public Transform itemsContainer;
    public Vector2 CellSize => _gridLayoutGroup.cellSize;
    [SerializeField] private Color32 _disallowColor;
    [SerializeField] private Color32 _allowColor;
    [SerializeField] private ItemTooltip _itemTooltip;
    [SerializeField] private ContextMenuUI _contextMenuUI;
    [SerializeField] private GameObject _itemPrefab;
    public GameObject ItemPrefab => _itemPrefab;
    private void Awake()
    {
        BuildMap();
    }
    
    private void BuildMap()
    {
        _cells = new InventoryCell[_size.x, _size.y];
        _map.Clear();

        foreach (var cell in _cellsList)
        {
            if(cell == null) continue;
            if (IsInside(cell.position))
            {
                _cells[cell.position.x, cell.position.y] = cell;
                _map[cell.position] = cell;
            }
            else
            {
                Debug.LogWarning($"Cell {cell.name} pos {cell.position} outside grid bounds");
            }
        }
    }

    public bool IsInside(Vector2Int p) => p.x >= 0 && p.y >= 0 && p.x < _size.x && p.y < _size.y;
    public InventoryCell GetCell(Vector2Int position) => _map.GetValueOrDefault(position);

    public bool CanPlaceAt(Vector2Int startPosition, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var p = new Vector2Int(startPosition.x + x, startPosition.y + y);
                if (!IsInside(p)) return false;
                var cell = GetCell(p);
                if (cell == null || !cell.IsEmpty) return false;
            }
        }
        return true;
    }

    public void PlaceItemAt(InventoryItemUI item, Vector2Int startPosition)
    {
        RemoveItem(item);
        item.grid = this;
        item.originalStartPos = startPosition;

        for (int x = 0; x < item.CurrentSize.x; x++)
        {
            for (int y = 0; y < item.CurrentSize.y; y++)
            {
                var p = new Vector2Int(startPosition.x + x, startPosition.y + y);
                var cell = GetCell(p);
                if(cell == null) continue;
                
                cell.SetItem(item);
                item.OccupiedCells.Add(cell);
            }
        }
        AnchorItemToCell(item);
    }

    public void RemoveItem(InventoryItemUI item)
    {
        if(item == null) return;
        
        foreach (var cell in item.OccupiedCells.ToList())
        {
            cell.Clear();
        }
        
        item.OccupiedCells.Clear();
    }

    public HashSet<InventoryItemUI> GetUniqueItemsInArea(Vector2Int startPost, Vector2Int size)
    {
        var set = new HashSet<InventoryItemUI>();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var p = new Vector2Int(startPost.x + x, startPost.y + y);
                var cell = GetCell(p);
                if (cell?.occupiedItem != null) set.Add(cell.occupiedItem);
            }
        }

        return set;
    }

    public bool TryFindSpace(Vector2Int size, out Vector2Int foundPos)
    {
        for (int x = 0; x <= _size.x - size.x; x++)
        {
            for (int y = 0; y <= _size.y - size.y; y++)
            {
                var p = new Vector2Int(x, y);
                if (CanPlaceAt(p, size))
                {
                    foundPos = p;
                    return true;
                }
            }
        }

        foundPos = default;
        return false;
    }


    private void AnchorItemToCell(InventoryItemUI item)
    {
        if(item.OccupiedCells.Count == 0) return;

        Vector3 avg = Vector3.zero;
        foreach (var cell in item.OccupiedCells)
        {
            avg += cell.Rect.position;
        }

        avg /= item.OccupiedCells.Count;
        item.Rect.position = avg;
        item.Rect.SetParent(itemsContainer, false);
    }

    public InventoryCell GetCellUnderPointer(Vector2 screenPos)
    {
        var ev = new PointerEventData(EventSystem.current) { position = screenPos };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ev,results);
        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent(out InventoryCell cell))
            {
                return cell;
            }

            var parentCell = result.gameObject.GetComponentInParent<InventoryCell>();
            if (parentCell != null) return parentCell;
        }
        return null;
    }

    public bool TryAddItem(ItemBaseScriptableObject data, GameObject itemPrefab, Transform parentOverride = null)
    {
        if (!TryFindSpace(data.Size, out Vector2Int pos))
        {
            Debug.Log("No space in inventory");
            return false;
        }
        
        Transform parent = parentOverride != null ? parentOverride : itemsContainer;
        GameObject obj = Instantiate(itemPrefab, parent);
        InventoryItemUI ui = obj.GetComponent<InventoryItemUI>();
        
        ui.grid = this;
        ui.tooltip = _itemTooltip;
        ui.ContextMenuUI = _contextMenuUI;
        ui.SetData(data);
        PlaceItemAt(ui, pos);
        
        return true;
    }
    public void HighlightArea(Vector2Int startPos, Vector2Int size)
    {
        bool ok = CanPlaceAt(startPos, size);
        Color c = ok ? _allowColor : _disallowColor;
        ClearAllHighlights();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var p = new Vector2Int(startPos.x + x, startPos.y + y);
                var cell = GetCell(p);
                cell?.Highlight(c, 0.45f);
            }
        }
    }

    public void ClearAllHighlights()
    {
        foreach (var c in _cellsList)
        {
            c?.ClearHighlight();
        }
    }
}
