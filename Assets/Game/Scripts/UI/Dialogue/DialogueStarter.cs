using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public DialogueNodeData startNode;
    public GameObject dialogueUI;
    public void Interact()
    {
        dialogueUI.SetActive(true);
        DialogueRunner.Instance.StartDialogue(startNode);
    }
}
