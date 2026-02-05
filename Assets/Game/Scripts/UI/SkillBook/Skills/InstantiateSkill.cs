using UnityEngine;

public class InstantiateSkill : BaseSkill
{
    private GameObject prefab;

    public InstantiateSkill(SkillScriptableObject data, GameObject prefab) : base(data)
    {
        this.prefab = prefab;
    }
    public override void Execute(SkillContext context)
    {
        GameObject obj = Object.Instantiate(prefab, context.castPoint.position,
            Quaternion.LookRotation(context.targetPoint - context.castPoint.position));
    }
}
