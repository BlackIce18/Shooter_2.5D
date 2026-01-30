using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillsDragAndDropController : MonoBehaviour
{
    public static SkillsDragAndDropController Instance;
    
    [SerializeField] private InventoryGrid _grid;
    [SerializeField] private Image _ghostImage;
    [SerializeField] private CanvasGroup _ghostCanvasGroup;
    
    private SkillUI _draggedItem;
    private InventoryGrid _originGrid;
    private Vector2Int _originStart;
    private bool _isDragging = false;
    
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
        
        /*var newGrid = GetGridUnderPointer(screenPos);

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
        }*/
    }
    public void StartDrag(SkillUI item)
    {
        if (item == null) return;

        _draggedItem = item;
        //_originGrid = item.grid;
        //_originStart = item.originalStartPos;

        //_originGrid?.RemoveItem(item);

        _ghostImage.sprite = item.SkillBookButtonUI.SkillScriptableObject.Icon;
        _ghostImage.rectTransform.sizeDelta = item.Rect.sizeDelta;
        _ghostImage.gameObject.SetActive(true);
        if (_ghostCanvasGroup != null) _ghostCanvasGroup.alpha = 0.9f;

        _isDragging = true;
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
