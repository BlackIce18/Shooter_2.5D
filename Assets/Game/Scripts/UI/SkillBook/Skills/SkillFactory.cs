using UnityEngine;

public static class SkillFactory
{
    public static ISkill Create(SkillScriptableObject data)
    {
        return data.SkillType switch
        {
            SkillType.Status => new StatusSkill(data),
            SkillType.Modifier => new ModifierSkill(data, data.buffData),
            SkillType.Instantiate => new InstantiateSkill(data, data.prefab),
            _ => null
        };
    }
}