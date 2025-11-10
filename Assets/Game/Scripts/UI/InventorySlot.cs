using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private bool _empty = true;

    public bool IsEmpty => _empty;
}
