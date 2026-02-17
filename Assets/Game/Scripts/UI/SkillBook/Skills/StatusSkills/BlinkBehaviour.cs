using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Behaviours/Blink")]
public class BlinkBehaviour : StatusBehaviour
{
    public override void Execute(SkillContext context)
    {
        context.casterAnimator.SetTrigger("AbilityUse");
    }
}
