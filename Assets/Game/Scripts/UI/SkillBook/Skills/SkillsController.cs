using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillsController : MonoBehaviour
{
    [SerializeField] private SkillContext _skillContext;
    public readonly float globalCooldown = 1f;
    public float currentGlobalCooldown;
    private bool _canCast = false;
    private Dictionary<string, float> _cooldowns = new();

    private void Update()
    {
        //if(!_canCast)
            currentGlobalCooldown -= Time.deltaTime;
        /*if (currentGlobalCooldown <= 0)
        {
            currentGlobalCooldown = globalCooldown;
            _canCast = true;
        }*/
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
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
        
        Vector3 position = Vector3.zero;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            position = hit.point;
            Debug.Log(position);
        }
        
        
        return new SkillContext
        {
            caster = _skillContext.caster,
            //target = _targetSystem.CurrentTarget,
            castPoint = transform.position,
            targetPoint = position,
            casterAnimator = _skillContext.casterAnimator
        };
    }

    public void TryCast(SkillScriptableObject data)
    {
        if(!CanCast(data)) return;

        SkillContext context = BuildContext();

        ISkill skill = SkillFactory.Create(data);
        skill.Execute(context);
        _canCast = false;
        RegisterCast(data);

    }
}