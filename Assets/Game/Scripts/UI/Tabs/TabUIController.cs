using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TabLink
{
    public Button button;
    public GameObject tab;
}
public class TabUIController : MonoBehaviour
{
    [SerializeField] private List<TabLink> _tabs;

    [SerializeField] private int _activeTab;
    [SerializeField] private Color32 _activeTabColor;
    [SerializeField] private Color32 _defaultTabColor;

    private void Start()
    {
        ChangeActiveTab();
    }

    public void ChangeActiveTab(int number = 0)
    {
        _activeTab = number;
        
        ResetToDefaultColor();
        CloseTabs();
        
        _tabs[_activeTab].button.image.color = _activeTabColor;
        _tabs[_activeTab].tab.SetActive(true);
    }

    private void ResetToDefaultColor()
    {
        for (int i = 0; i < _tabs.Count; i++)
        {
            _tabs[i].button.image.color = _defaultTabColor;
        }
    }

    private void CloseTabs()
    {
        for (int i = 0; i < _tabs.Count; i++)
        {
            _tabs[i].tab.SetActive(false);
        }
    }
}
