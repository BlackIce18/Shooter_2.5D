using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
[RequireComponent(typeof(CanvasGroup))]
public class SkillUI : SkillUIBase
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
        Image.sprite = _skillBookButtonUI.SkillScriptableObject.Icon;
        LockSkill();
        canvasGroup = GetComponent<CanvasGroup>();
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
        _upgradeButton.interactable = true;
        _upgradeButton.gameObject.SetActive(true);
    }

    public void DisableUpgradeButton()
    {
        _upgradeButton.interactable = false;
        _upgradeButton.gameObject.SetActive(false);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        SkillsDragAndDropController.Instance?.OnDragUpdate(eventData.position);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        SkillsDragAndDropController.Instance?.StartDrag(this);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On end drag");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {        
        Debug.Log("click");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
    }

    public override void OnPointerMove(PointerEventData eventData)
    {
        Debug.Log("move");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
    }
}
