using System;
using System.Collections.Generic;
using UnityEngine;



public class SkillBookPageUI : MonoBehaviour
{
    [SerializeField] private List<SkillUI> _skillsList;

    public List<SkillUI> SkillsList => _skillsList;
    private void Start()
    {
        for (int i = 0; i < _skillsList.Count; i++)
        {
            _skillsList[i].LockSkill();
        }
    }
    
}
