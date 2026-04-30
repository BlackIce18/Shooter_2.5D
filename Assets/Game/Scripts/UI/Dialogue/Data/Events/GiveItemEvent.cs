using UnityEngine;

public class GiveItemEvent : DialogueEvent
{
    public ItemBaseScriptableObject item;

    public override void Execute()
    {
        //InventoryManager.AddItem(item);
    }
}
