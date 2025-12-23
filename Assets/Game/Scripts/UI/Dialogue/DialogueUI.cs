using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private Button choiceButtonPrefab;

    public void ShowText(string text)
    {
        gameObject.SetActive(true);
        dialogueText.text = text;
    }

    public void ShowChoices(List<DialogueChoiceData> choices, Action<DialogueChoiceData> onSelect)
    {
        ClearChoices();

        foreach (var choice in choices)
        {
            var btn = Instantiate(choiceButtonPrefab, choicesContainer);
            btn.GetComponentInChildren<TMP_Text>().text = LocalizationManager.Instance.Get(choice.textKey);
            
            btn.onClick.AddListener(()=>onSelect(choice));
        }
    }

    public void Hide()
    {
        ClearChoices();
        gameObject.SetActive(false);
    }
    
    private void ClearChoices()
    {
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
