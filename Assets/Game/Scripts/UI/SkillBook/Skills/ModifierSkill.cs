using UnityEngine;

public class ModifierSkill : BaseSkill
{
    private Buff _buff;

    public ModifierSkill(SkillScriptableObject data, Buff buff) : base(data)
    {
        _buff = buff;
    }

    public override void Execute(SkillContext context)
    {
        context.buffController.Apply(_buff, context.buffController.ActiveBuffs);
    }
}
