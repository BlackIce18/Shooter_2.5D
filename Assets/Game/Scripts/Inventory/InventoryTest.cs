using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public InventoryGrid grid;
    public EquipmentItems sword;
    public GameObject itemPrefab;
    
    private void Start()
    {
        grid.TryAddItem(sword, itemPrefab);
    }
}
