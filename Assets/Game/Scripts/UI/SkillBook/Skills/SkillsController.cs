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
}