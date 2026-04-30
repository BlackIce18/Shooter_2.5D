using UnityEngine;

public class SkillPanelKeybind : KeyCommand
{
    [SerializeField] private SkillScriptableObject _skillData;
    [SerializeField] private SkillsController _skillsController;
    public override void Execute()
    {
        _skillsController.TryCast(_skillData);
    }
}
