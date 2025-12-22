using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "DialogueGraph",
    menuName = "Dialogue/Dialogue Graph")]
public class DialogueGraph : ScriptableObject
{
    public string startNodeGuid;
    public List<DialogueNode> nodes = new();

    public DialogueNode GetNode(string guid) => nodes.Find(n => n.guid == guid);
}
