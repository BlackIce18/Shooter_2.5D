using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    //[SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private GameObject _choiceElement;
    [SerializeField] private Transform _choicesContainer;
    [SerializeField] private Button _choiceButtonPrefab;
    [SerializeField] private DialogueHistory _dialogueHistory;
    
    public void ShowText(string text)
    {
        gameObject.SetActive(true);
        _dialogueHistory.AddToHistory(text);
        //_dialogueText.text = text;
    }

    public void ShowChoices(List<DialogueChoiceData> choices, Action<DialogueChoiceData> onSelect)
    {
        if(choices.Count == 0) return;
        ChoiceElement choiceElement = Instantiate(_choiceElement, _dialogueHistory.dialogueElementParent.transform).GetComponent<ChoiceElement>();
        _choicesContainer = choiceElement.Container.transform;
        
        ClearChoices();

        foreach (var choice in choices)
        {
            var btn = Instantiate(_choiceButtonPrefab, _choicesContainer);
            btn.GetComponentInChildren<TMP_Text>().text = LocalizationManager.Instance.Get(choice.textKey);
            
            btn.onClick.AddListener(()=>
            {
                onSelect(choice);
                //_dialogueHistory.AddToHistory(LocalizationManager.Instance.Get(choice.textKey));
            });
        }
    }

    public void Hide()
    {
        ClearChoices();
        _dialogueHistory.Clear();
        gameObject.SetActive(false);
    }
    
    private void ClearChoices()
    {
        foreach (Transform child in _choicesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
