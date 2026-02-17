using UnityEngine;

public class StatusSkill : BaseSkill
{
    public StatusSkill(SkillScriptableObject data) : base(data) {}
    
    public override void Execute(SkillContext context)
    {
        data.statusBehaviour.Execute(context);
    }
}
