using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "DialogueGraph",
    menuName = "Dialogue/Dialogue Graph")]
public class DialogueGraph : ScriptableObject
{
    public string startNodeGuid;
    public List<DialogueNodeData> nodes = new();

    public DialogueNodeData GetNode(string guid) => nodes.Find(n => n.textKey == guid);
}
