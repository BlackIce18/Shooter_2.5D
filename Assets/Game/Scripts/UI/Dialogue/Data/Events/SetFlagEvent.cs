using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Events/Set Flag")]
public class SetFlagEvent : DialogueEvent
{
    public string flagId;

    public override void Execute()
    {
        GameFlags.SetFlag(flagId);
    }
}
