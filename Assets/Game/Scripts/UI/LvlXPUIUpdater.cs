using System;
using TMPro;
using UnityEngine;

public class LvlXPUIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lvl;
    [SerializeField] private TextMeshProUGUI _currentXp;
    [SerializeField] private LVLSystem _lvlSystem;

    private void Start()
    {
        UpdateLVLAndXP();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<UpdateLvlXpEvent>(AddXpLvl);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<UpdateLvlXpEvent>(AddXpLvl);
    }

    private void AddXpLvl(UpdateLvlXpEvent lvlXp)
    {
        UpdateLVLAndXP();
    }
    
    private void UpdateLVLAndXP()
    {
        _currentXp.text = _lvlSystem.CurrentXp + " / " + _lvlSystem.LvlList[_lvlSystem.CurrentLvl - 1].xpToLvl;
        _lvl.text = _lvlSystem.CurrentLvl.ToString();
    }
}
