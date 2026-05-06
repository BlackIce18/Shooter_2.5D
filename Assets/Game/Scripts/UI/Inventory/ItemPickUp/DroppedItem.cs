using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class DroppedItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private ItemBaseScriptableObject itemBaseScriptableObject;
    public ItemBaseScriptableObject ItemBaseScriptableObject => itemBaseScriptableObject;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Условия:
            // Расстояние до игрока < n
            // Есть место в инвентаре для расположения предмета
            
            EventBus.Publish(new PickUpItemEvent(ItemBaseScriptableObject));
            Destroy(gameObject);
            Global.Instance.UI.DropFloatingWindow.transform.gameObject.SetActive(false);
            Global.Instance.UI.DropFloatingWindow.Hide();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Global.Instance.UI.DropFloatingWindow.transform.gameObject.SetActive(true);
        Global.Instance.UI.DropFloatingWindow.ChangeText(ItemBaseScriptableObject.Name);
        Global.Instance.UI.DropFloatingWindow.Show();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Global.Instance.UI.DropFloatingWindow.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Global.Instance.UI.DropFloatingWindow.transform.gameObject.SetActive(false);
        Global.Instance.UI.DropFloatingWindow.Hide();
    }
}
