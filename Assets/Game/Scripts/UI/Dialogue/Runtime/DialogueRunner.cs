using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueRunner : MonoBehaviour
{
    public static DialogueRunner Instance;

    [SerializeField] private DialogueUI ui;
    private DialogueNodeData _currentNode;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDialogue(DialogueNodeData startNode)
    {
        ShowNode(startNode);
    }

    private void ShowNode(DialogueNodeData node)
    {
        if(!CheckConditions(node.conditions)) return;

        _currentNode = node;
        
        ExecuteEvents(node.events);

        ui.ShowText(LocalizationManager.Instance.Get(node.textKey));
        ui.ShowChoices(node.choices.Where(c => CheckConditions(c.conditions)).ToList(), OnChoiceSelected);
    }

    private void OnChoiceSelected(DialogueChoiceData choice)
    {
        ExecuteEvents(choice.events);

        if (choice.nextNode != null)
        {
            ShowNode(choice.nextNode);
        }
        else
        {
            ui.Hide();
        }
    }

    private bool CheckConditions(List<DialogueCondition> conditions)
    {
        foreach (var c in conditions)
        {
            if (!c.Check()) return false;
        }
        
        return true;
    }

    private void ExecuteEvents(List<DialogueEvent> events)
    {
        foreach (var e in events)
        {
            e.Execute();
        }
    }
}
