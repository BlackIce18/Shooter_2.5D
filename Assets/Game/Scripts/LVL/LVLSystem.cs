using System;
using System.Collections.Generic;
using SingularityGroup.HotReload;
using UnityEngine;

public class LVLSystem : MonoBehaviour
{
    [SerializeField] private int _currentLvl;
    [SerializeField] private int _currentXp;
    [SerializeField] private List<XpScriptableObject> _lvlList;

    public int CurrentLvl => _currentLvl;
    public int CurrentXp => _currentXp;

    public List<XpScriptableObject> LvlList => _lvlList;

    private void OnEnable()
    {
        EventBus.Subscribe<AddXpEvent>(AddXp);
        EventBus.Subscribe<SubXpEvent>(SubXp);
       /*EventBus.Subscribe<LvlUpEvent>(LvlUp);
        EventBus.Subscribe<LvlDownEvent>(LvlDown);*/
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<AddXpEvent>(AddXp);
        EventBus.Unsubscribe<SubXpEvent>(SubXp);
        /*EventBus.Unsubscribe<LvlUpEvent>(LvlUp);
        EventBus.Unsubscribe<LvlDownEvent>(LvlDown);*/
    }

    private void AddXp(AddXpEvent addXp)
    {
        if(_currentLvl == _lvlList.Count && _currentXp == _lvlList[_currentLvl - 1].xpToLvl) return;
        _currentXp += addXp.xp;
        if (_currentLvl == _lvlList.Count)
        {
            if (_currentXp >= _lvlList[_currentLvl - 1].xpToLvl)
            {
                _currentXp = _lvlList[_currentLvl - 1].xpToLvl;
                EventBus.Publish(new UpdateLvlXpEvent(_currentXp, _currentLvl));
                return;
            }
        }
        
        while (_currentXp >= _lvlList[_currentLvl - 1].xpToLvl)
        {
            _currentXp -= _lvlList[_currentLvl - 1].xpToLvl;
            if (_currentLvl <= _lvlList.Count - 1)
            {
                _currentLvl++; 
                EventBus.Publish(new LvlUpEvent());
                
            }
        }
        EventBus.Publish(new UpdateLvlXpEvent(_currentXp, _currentLvl));
    }

    private void SubXp(SubXpEvent subXp)
    {
        if (_currentLvl == 1 && _currentXp == 0)
        {
            EventBus.Publish(new UpdateLvlXpEvent(_currentXp, _currentLvl));
            return;
        }
        
        _currentXp -= subXp.xp;
        if (_currentXp < 0) _currentXp = 0;
        EventBus.Publish(new UpdateLvlXpEvent(_currentXp, _currentLvl));
    }

    public void LvlUp(LvlUpEvent lvlUpEvent)
    {
        LvlUp();
    }
    
    public void LvlDown(LvlDownEvent lvlDownEvent)
    {
        LvlDown();
    }
    
    public void LvlUp()
    {
        _currentLvl++;
        EventBus.Publish(new LvlUpEvent());
        EventBus.Publish(new UpdateLvlXpEvent(_currentXp, _currentLvl));
    }

    public void LvlDown()
    {
        _currentLvl--;
        EventBus.Publish(new UpdateLvlXpEvent(_currentXp, _currentLvl));
    }
}
