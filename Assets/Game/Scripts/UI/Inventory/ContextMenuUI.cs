using UnityEngine;
using UnityEngine.UI;

public class ContextMenuUI : MonoBehaviour
{
    [SerializeField] private Button _useButton;

    public void EquipAction(InventoryItemUI inventoryItemUI)
    {
        _useButton.onClick.RemoveAllListeners();
        _useButton.onClick.AddListener(delegate
        {
            inventoryItemUI.data.Use();
            inventoryItemUI.grid.RemoveItem(inventoryItemUI);
            Destroy(inventoryItemUI.gameObject);
            
            gameObject.SetActive(false);
        });
    }
}
