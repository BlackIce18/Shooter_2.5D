using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueRunner : MonoBehaviour
{
    [Header("Data")] [SerializeField] private DialogueGraph _graph;

    [Header("UI")] 
    [SerializeField] private TMP_Text _dialogueText;

    [SerializeField] private Transform _choicesContainer;
    [SerializeField] private Button _choiceButtonPrefab;

    private DialogueNode _currentNode;

    private void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        _currentNode = _graph.GetNode(_graph.startNodeGuid);
        ShowNode(_currentNode);
    }

    private void ShowNode(DialogueNode node)
    {
        ClearChoices();

        _dialogueText.text = LocalizationManager.Instance.Get(node.textKey);

        foreach (var choice in node.choices)
        {
            CreateChoice(choice);
        }
    }

    private void CreateChoice(DialogueChoice choice)
    {
        var btn = Instantiate(_choiceButtonPrefab, _choicesContainer);
        btn.GetComponentInChildren<TMP_Text>().text = LocalizationManager.Instance.Get(choice.choiceKey);
        
        btn.onClick.AddListener(() =>
        {
            var next = _graph.GetNode(choice.targetNodeGuid);
            if (next != null)
            {
                _currentNode = next;
                ShowNode(_currentNode);
            }
        });
    }

    private void ClearChoices()
    {
        foreach (Transform choice in _choicesContainer)
        {
            Destroy(choice.gameObject);
        }
    }
}
