using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public InventoryGrid grid;
    public EquipmentItems sword;
    public EquipmentItems second;
    public GameObject itemPrefab;
    
    private void Start()
    {
        grid.TryAddItem(second, itemPrefab);
        grid.TryAddItem(second, itemPrefab);
        grid.TryAddItem(sword, itemPrefab);
        grid.TryAddItem(sword, itemPrefab);
        grid.TryAddItem(sword, itemPrefab);
    }
}
