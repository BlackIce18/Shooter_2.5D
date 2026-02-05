using UnityEngine;

public class StatusSkill : BaseSkill
{
    // Блинк за спину, Телепорт,ледянные следы
    public StatusSkill(SkillScriptableObject data) : base(data) {}
    
    public override void Execute(SkillContext context)
    {
        context.caster.transform.position = context.targetPoint;
    }
}
