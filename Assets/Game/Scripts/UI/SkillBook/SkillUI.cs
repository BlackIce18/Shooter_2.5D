using System;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class SkillBookButtonUI
{
    [SerializeField] private GameObject _skillGameObject;
    [SerializeField] private SkillUI _skillUI;
    [SerializeField] private int _currentTier = 1;
    [SerializeField] private skillScriptableObject _skillScriptableObject;

    public GameObject SkillGameObject => _skillGameObject;
    public SkillUI SkillUI => _skillUI;
    public int currentTier
    {
        get => _currentTier;
        set => _currentTier = value;
    }
    public bool CanUpgrade => currentTier < SkillScriptableObject.MaxTier;
    public skillScriptableObject SkillScriptableObject => _skillScriptableObject;
}
public class SkillUI : MonoBehaviour
{
    public bool isAvailable;
    [SerializeField] private Button _button;
    [SerializeField] private Button _upgradeButton;
    //[SerializeField] private skillScriptableObject _skill;
    [SerializeField] private SkillBookPageUI _page;
    [SerializeField] private SkillBookButtonUI _skillBookButtonUI;
    public SkillBookButtonUI SkillBookButtonUI => _skillBookButtonUI;
    public Button UpgradeButton => _upgradeButton;
    private void Start()
    {
        LockSkill();
    }

    public void LockSkill()
    {
        if (isAvailable == false)
        {
            _button.interactable = false;
        }
        else
        {

        }
    }

    public void UnlockSkill()
    {
        if (isAvailable == false)
        {
           return;
        }
        _button.interactable = true;
    }

    public void UnlockToUpgrade()
    {
        Debug.Log("2");
        _upgradeButton.interactable = true;
        _upgradeButton.gameObject.SetActive(true);
    }

    public void DisableUpgradeButton()
    {
        _upgradeButton.interactable = false;
        _upgradeButton.gameObject.SetActive(false);
    }
}
