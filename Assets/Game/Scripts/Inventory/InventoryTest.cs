using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public InventoryGrid grid;
    public EquipmentItemScriptableObject sword;
    public EquipmentItemScriptableObject second;
    public EquipmentItemScriptableObject t;
    
    private void Start()
    {
        grid.TryAddItem(second, grid.ItemPrefab);
        grid.TryAddItem(second, grid.ItemPrefab);
        grid.TryAddItem(sword, grid.ItemPrefab);
        grid.TryAddItem(sword, grid.ItemPrefab);
        grid.TryAddItem(sword, grid.ItemPrefab);
        grid.TryAddItem(t, grid.ItemPrefab);
    }
}
