using UnityEngine;

public class SkillPanelKeybind : KeyCommand
{
    [SerializeField] private BaseSkill _baseSkill;
    [SerializeField] private SkillsController _skillsController;
    public override void Execute()
    {
        _skillsController.TryCast(_baseSkill.Data);
    }
}
