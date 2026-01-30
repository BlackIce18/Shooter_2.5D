using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillBookUI : KeyCommand
{
    [SerializeField] private List<SkillBookButtonUI> _unlockedSkills;
    [SerializeField] private List<SkillBookPageUI> _skillbookPages;
    [SerializeField] private LVLSystem _lvlSystem;
    public int availableUnlockSkillCount = 0;
    [SerializeField] private TextMeshProUGUI _text;
    private void OnEnable()
    {
        EventBus.Subscribe<LvlUpEvent>(LvlUp);
        EventBus.Subscribe<UnlockSkillEvent>(UnlockSkillEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LvlUpEvent>(LvlUp);
        EventBus.Unsubscribe<UnlockSkillEvent>(UnlockSkillEvent);
    }

    private void LvlUp(LvlUpEvent e)
    {
        availableUnlockSkillCount++;
        OpenPage();
        _text.text = availableUnlockSkillCount.ToString();
    }
    private void Start()
    {
        OpenPage();
    }

    public void OpenPage()
    {
        for (int i = 0; i < _unlockedSkills.Count; i++)
        {
            _unlockedSkills[i].SkillUI.isAvailable = true;
            _unlockedSkills[i].SkillUI.UnlockSkill();
        }
        foreach (var page in _skillbookPages)
        {
            foreach (var skill in page.SkillsList)
            {
                var button = skill.SkillBookButtonUI;
                var skillSO = button.SkillScriptableObject;
                
                if(!button.CanUpgrade) continue;
                if (availableUnlockSkillCount <= 0)
                {
                    skill.DisableUpgradeButton();
                    continue;
                }
                if(_lvlSystem.CurrentLvl < skillSO.RequiredLvl) continue;
                if (skillSO.RequiredUnlockedSkill != null &&
                    !_unlockedSkills.Exists(u=> u.SkillScriptableObject == skillSO.RequiredUnlockedSkill)) continue;
                
                skill.SkillBookButtonUI.SkillUI.UnlockToUpgrade();
            }
        }
    }

    public override void Execute()
    {
        OpenPage();
    }
    
    public void UnlockSkillEvent(UnlockSkillEvent e)
    {
        var button = e.skillUI.SkillBookButtonUI;

        if (_unlockedSkills.Contains(button))
        {
            if(!button.CanUpgrade) return;

            button.currentTier++;
        }
        else
        {
            _unlockedSkills.Add(button);
            e.skillUI.isAvailable = true;
            button.SkillGameObject.SetActive(true);
            e.skillUI.UnlockSkill();
        }
        
        availableUnlockSkillCount--;
        if (availableUnlockSkillCount <= 0 || !e.skillUI.SkillBookButtonUI.CanUpgrade)
        {
            e.skillUI.UpgradeButton.gameObject.SetActive(false);
        }
        _text.text = availableUnlockSkillCount.ToString();

        Execute();
    }
}
