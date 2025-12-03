using UnityEngine;
using UnityEngine.UI;

public class ContextMenuUI : MonoBehaviour
{
    [SerializeField] private Button _useButton;

    public void BindAction(InventoryItemUI inventoryItemUI)
    {
        _useButton.onClick.RemoveAllListeners();
        _useButton.onClick.AddListener(delegate
        {
            inventoryItemUI.data.Use();
            gameObject.SetActive(false);
        });
        
    }
}
