using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public Vector2Int position;
    public InventoryItemUI occupiedItem;
    [SerializeField] private Image _image;
    [SerializeField] private Image _highlightOverlay;
    public RectTransform Rect => (RectTransform)transform;
    public bool IsEmpty => occupiedItem == null;
    public Image Image => _image;

    public void SetItem(InventoryItemUI item)
    {
        occupiedItem = item;
        
        if(item != null && !item.OccupiedCells.Contains(this))
            item.OccupiedCells.Add(this);
    }

    public void Clear()
    {
        if (occupiedItem != null)
        {
            occupiedItem.OccupiedCells.Remove(this);
        }

        occupiedItem = null;
    }

    public void Highlight(Color color, float alpha = 0.6f)
    {
        if(_highlightOverlay == null) return;
        _highlightOverlay.gameObject.SetActive(true);
        _highlightOverlay.color = new Color(color.r, color.g, color.b, alpha);
    }

    public void ClearHighlight()
    {
        if(_highlightOverlay == null) return;
        _highlightOverlay.gameObject.SetActive(false);
    }
}
