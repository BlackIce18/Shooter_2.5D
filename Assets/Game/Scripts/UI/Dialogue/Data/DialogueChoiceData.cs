using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoiceData : MonoBehaviour
{
    public string textKey;
    public DialogueNodeData nextNode;

    public List<DialogueCondition> conditions = new();
    public List<DialogueEvent> events = new();
}