using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
    public Equipment _equipmentTest;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InventorySlot target))
        {
            Debug.Log(target);
            if (target.IsEmpty)
            {
                
            }
        }
    }
}
