using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public DialogueNodeData startNode;

    public void Interact()
    {
        DialogueRunner.Instance.StartDialogue(startNode);
    }
}
