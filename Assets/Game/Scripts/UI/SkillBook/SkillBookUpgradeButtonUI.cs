using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillBookUpgradeButtonUI : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private SkillUI _skillUI;
    private void Start()
    {
        _upgradeButton.onClick.AddListener(Execute);
    }

    private void Execute()
    {
        var skill = _skillUI.SkillBookButtonUI;
        EventBus.Publish(new UnlockSkillEvent(skill.SkillGameObject, skill.SkillUI, skill.currentTier, skill.SkillScriptableObject));
    }
}
