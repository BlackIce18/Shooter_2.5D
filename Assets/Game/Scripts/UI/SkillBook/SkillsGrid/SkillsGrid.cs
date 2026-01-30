using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;

    public Vector2Int Size => _size;

    [SerializeField] private List<SkillCell> _cellsList = new();
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    private SkillCell[,] _cells;
    private Dictionary<Vector2Int, SkillCell> _map = new();

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
        _cells = new SkillCell[_size.x, _size.y];
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
    public SkillCell GetCell(Vector2Int position) => _map.GetValueOrDefault(position);
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
    public void PlaceItemAt(SkillUI item, Vector2Int startPosition)
    {
        RemoveItem(item);
        item.grid = this;
        item.originalStartPos = startPosition;

        var p = new Vector2Int(startPosition.x, startPosition.y);
        var cell = GetCell(p);
        // if(cell == null) continue;
                
        cell.SetItem(item);
        item.occupiedCell = cell;
        
        AnchorItemToCell(item);
    }
    public void RemoveItem(SkillUI item)
    {
        if(item == null) return;
        
        item.occupiedCell.Clear();
    }
    private void AnchorItemToCell(SkillUI item)
    {
        if(item.occupiedCell == null) return;
        
        item.Rect.position = item.occupiedCell.Rect.position;
        item.Rect.SetParent(itemsContainer, true);
    }
}
