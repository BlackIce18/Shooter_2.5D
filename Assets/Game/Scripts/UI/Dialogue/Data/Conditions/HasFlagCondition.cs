using UnityEngine;

public class HasFlagCondition : DialogueCondition
{
    public string flagId;

    public override bool Check()
    {
        return GameFlags.HasFlag(flagId);
    }
}
