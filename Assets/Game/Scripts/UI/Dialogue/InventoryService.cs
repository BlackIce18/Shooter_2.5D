using UnityEngine;

public static class InventoryService
{
    public static void AddItem(ItemBaseScriptableObject item)
    {
        Debug.Log($"Получен предмет: {item.name}");
    }
}
