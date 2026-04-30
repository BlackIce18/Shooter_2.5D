using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Node")]
public class DialogueNodeData : ScriptableObject
{
    [Header("Localization")]
    public string textKey;

    [Header("Choices")] public List<DialogueChoiceData> choices = new();

    [Header("Conditions")] public List<DialogueCondition> conditions = new();
    [Header("Events")] public List<DialogueEvent> events = new();
}
