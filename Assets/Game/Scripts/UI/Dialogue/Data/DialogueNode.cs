using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class DialogueNode
{
    public string guid;
    public Vector2 position;

    public string textKey;

    public List<DialogueChoice> choices = new();
}
