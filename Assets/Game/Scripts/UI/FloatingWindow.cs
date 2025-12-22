using UnityEngine;
using UnityEngine.InputSystem;
public class FloatingWindow : MonoBehaviour
{
    [SerializeField] private RectTransform  _rect;
    [SerializeField] private Vector2 _tooltipOffset;
    [SerializeField] private Canvas _canvas;
    private void Start()
    {
        _canvas = gameObject.GetComponentInParent<Canvas>();
    }

    public virtual void Show()
    {
        if (_rect == null) return;
        
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 anchoredPos;
        
        RectTransform canvasRect = _canvas.transform as RectTransform;
        if (canvasRect == null)
        {
            Debug.LogError("Canvas не является RectTransform!");
            return;
        }
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mousePosition,
            null,
            out anchoredPos
        );
        anchoredPos += new Vector2(_rect.rect.width / 2f + _tooltipOffset.x, -_rect.rect.height / 2f - _tooltipOffset.y);
        _rect.anchoredPosition = anchoredPos;
        
        _rect.gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
