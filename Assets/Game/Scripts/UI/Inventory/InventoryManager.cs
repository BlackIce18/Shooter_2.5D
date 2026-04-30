using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventoryGrid grid;
    public EquipmentItemBaseScriptableObject sword;
    public EquipmentItemBaseScriptableObject second;
    public EquipmentItemBaseScriptableObject t;
    public Canvas inventory;
    private void Start()
    {
        grid.TryAddItem(second, grid.ItemPrefab);
        grid.TryAddItem(second, grid.ItemPrefab);
        grid.TryAddItem(sword, grid.ItemPrefab);
        grid.TryAddItem(sword, grid.ItemPrefab);
        grid.TryAddItem(sword, grid.ItemPrefab);
        grid.TryAddItem(t, grid.ItemPrefab);
    }
    private void OnEnable()
    {
        EventBus.Subscribe<PickUpItemEvent>(PickupEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PickUpItemEvent>(PickupEvent);
    }

    private void PickupEvent(PickUpItemEvent pickUpItemEvent)
    {
        grid.TryAddItem(pickUpItemEvent.ItemBaseScriptableObject, grid.ItemPrefab);
    }
}
