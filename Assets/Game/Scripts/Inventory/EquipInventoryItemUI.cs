using UnityEngine;
using UnityEngine.EventSystems;
public class EquipInventoryItemUI : InventoryItemUI
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            tooltip.Hide();
            EventBus.Publish(new TryUnEquipEvent(data.EquipmentType, data));
        }
    }
}
