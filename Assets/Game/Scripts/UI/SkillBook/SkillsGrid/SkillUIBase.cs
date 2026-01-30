using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SkillUIBase : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    public Image Image;
    public RectTransform Rect => (RectTransform)transform;
    [HideInInspector] public CanvasGroup canvasGroup;
    [HideInInspector] public SkillCell occupiedCell;
    public const int Size = 1;
    public SkillsGrid grid;
    [HideInInspector] public Vector2Int originalStartPos;
    public abstract void OnDrag(PointerEventData eventData);
    public abstract void OnBeginDrag(PointerEventData eventData);
    public abstract void OnEndDrag(PointerEventData eventData);
    public abstract void OnPointerClick(PointerEventData eventData);
    public abstract void OnPointerEnter(PointerEventData eventData);
    public abstract void OnPointerMove(PointerEventData eventData);
    public abstract void OnPointerExit(PointerEventData eventData);
}
