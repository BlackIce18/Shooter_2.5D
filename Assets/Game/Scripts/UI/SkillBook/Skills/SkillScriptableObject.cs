using UnityEngine;

public enum SkillType
{
    Status,
    Modifier,
    Instantiate
}

[CreateAssetMenu(menuName = "Skills/Skill")]
public class SkillScriptableObject : ScriptableObject
{
    [Header("Base")]
    public string id;
    public string name;
    public string description;
    public Sprite icon;
    
    [Header("Timing")]
    public float cooldown;
    public float castTime = 0;
    [Space]
    public float distance;
    
    [Header("Cost")]
    public float manaCost;
    public float hpCost;
    public ItemBaseScriptableObject requiredItem;
    [Space]
    public SkillType SkillType;
    [Header("Modifier Skill")]
    public Buff buffData;

    [Header("Instantiate Skill")]
    public GameObject prefab;
}












