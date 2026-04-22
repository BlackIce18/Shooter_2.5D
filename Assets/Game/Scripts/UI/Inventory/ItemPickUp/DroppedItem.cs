using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class DroppedItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private ItemBaseScriptableObject itemBaseScriptableObject;
    public ItemBaseScriptableObject ItemBaseScriptableObject => itemBaseScriptableObject;
    public FloatingWindow windowUI;
    public TextMeshProUGUI textField;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Условия:
            // Расстояние до игрока < n
            // Есть место в инвентаре для расположения предмета
            
            EventBus.Publish(new PickUpItemEvent(ItemBaseScriptableObject));
            Destroy(gameObject);
            windowUI.transform.gameObject.SetActive(false);
            windowUI.Hide();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        windowUI.transform.gameObject.SetActive(true);
        textField.text = ItemBaseScriptableObject.Name;
        windowUI.Show();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        windowUI.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        windowUI.transform.gameObject.SetActive(false);
        windowUI.Hide();
    }
}
