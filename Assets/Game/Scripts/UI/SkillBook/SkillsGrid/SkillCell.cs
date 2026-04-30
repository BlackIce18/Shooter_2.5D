using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour
{
    public Vector2Int position;
    public SkillUI occupiedSkill;
    [SerializeField] private Image _image;
    [SerializeField] private Image _highlightOverlay;
    public RectTransform Rect => (RectTransform)transform;
    public bool IsEmpty => occupiedSkill == null;
    public Image Image => _image;
    
    public void SetItem(SkillUI item)
    {
        occupiedSkill = item;

        if (item != null && item.occupiedCell == null)
            item.occupiedCell = this;
    }

    public void Clear()
    {
        if (occupiedSkill != null) occupiedSkill = null;
    }

    public void Highlight(Color color, float alpha = 0.6f)
    {
        if(_highlightOverlay == null) return;
        _highlightOverlay.gameObject.SetActive(true);
        _highlightOverlay.color = new Color(color.r, color.g, color.b, alpha);
    }
}
