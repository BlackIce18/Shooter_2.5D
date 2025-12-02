using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItem : InventoryItemUI
{
    private HashSet<InventoryCell> _hitTargets = new HashSet<InventoryCell>();
    public Equipment _equipmentTest;
    public event System.Action<InventoryCell> OnHit;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InventoryCell target))
        {
            Debug.Log(target);
            if (!_hitTargets.Contains(target))
            {
                _hitTargets.Add(target);
                
                OnHit?.Invoke(target);
            }
        }
    }
}