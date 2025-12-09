using UnityEngine;
using UnityEngine.EventSystems;
public class DroppedItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private ItemBaseScriptableObject itemBaseScriptableObject;
    public ItemBaseScriptableObject ItemBaseScriptableObject => itemBaseScriptableObject;
    public FloatingWindow windowUI;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            EventBus.Publish(new PickUpItemEvent(ItemBaseScriptableObject));
            Destroy(gameObject);
            windowUI.transform.parent.gameObject.SetActive(false);
            windowUI.Hide();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        windowUI.transform.parent.gameObject.SetActive(true);
        windowUI.Show();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        windowUI.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        windowUI.transform.parent.gameObject.SetActive(false);
        windowUI.Hide();
    }
}
