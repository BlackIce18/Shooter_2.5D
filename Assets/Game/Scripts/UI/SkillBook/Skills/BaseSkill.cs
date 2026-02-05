using UnityEngine;

public abstract class BaseSkill : ISkill
{
    protected SkillScriptableObject data;
    protected BaseSkill(SkillScriptableObject data) { this.data = data;}
    public abstract void Execute(SkillContext context);
}

