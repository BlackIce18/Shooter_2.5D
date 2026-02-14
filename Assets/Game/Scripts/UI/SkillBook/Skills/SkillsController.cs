using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{
    public readonly float globalCooldown = 1f;
    public float currentGlobalCooldown;

    private Dictionary<string, float> _cooldowns = new();

    private void Update()
    {
        currentGlobalCooldown -= Time.deltaTime;
    }

    public bool CanCast(SkillScriptableObject skill)
    {
        if (currentGlobalCooldown > 0) return false;

        if (_cooldowns.TryGetValue(skill.id, out float cd)) return cd <= Time.time;

        return true;
    }

    public void RegisterCast(SkillScriptableObject skill)
    {
        currentGlobalCooldown = globalCooldown;
        _cooldowns[skill.id] = Time.time + skill.cooldown;
    }

    private SkillContext BuildContext()
    {
        return new SkillContext
        {
            /*caster = this.gameObject,
            //target = _targetSystem.CurrentTarget,
            castPoint = transform.position,
            targetPoint =*/
        };
    }

    public void TryCast(SkillScriptableObject data)
    {
        if(!CanCast(data)) return;

        SkillContext context = BuildContext();

        ISkill skill = SkillFactory.Create(data);
        skill.Execute(context);
        
        RegisterCast(data);

    }
}