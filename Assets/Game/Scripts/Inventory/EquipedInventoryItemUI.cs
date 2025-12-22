using UnityEngine;
using UnityEngine.EventSystems;
public class EquipedInventoryItemUI : InventoryItemUI
{
    [SerializeField] private EquipmentType equipmentType;
    [SerializeField] private EquipmentItemBaseScriptableObject _data;
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            tooltip.Hide();
            EventBus.Publish(new TryUnEquipEvent(equipmentType, _data, this));
            Debug.Log("Из экипировки в инвентарь");
        }
    }
    
    public override void OnDrag(PointerEventData eventData)
    {
        DragAndDropController.Instance?.OnDragUpdate(eventData.position);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        DragAndDropController.Instance?.StartDrag(this);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        DragAndDropController.Instance?.EndDrag();
    }
}
