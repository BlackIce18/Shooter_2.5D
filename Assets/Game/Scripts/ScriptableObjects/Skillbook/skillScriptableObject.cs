using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Attack")]
public class skillScriptableObject : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _requiredLvl;
    [SerializeField] private int _tier = 1;
    [SerializeField] private int _maxTier = 5;
    [SerializeField] private skillScriptableObject _requiredUnlockedSkill;

    public int RequiredLvl => _requiredLvl;
    public skillScriptableObject RequiredUnlockedSkill => _requiredUnlockedSkill;
    public int MaxTier => _maxTier;
}