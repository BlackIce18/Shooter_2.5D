using UnityEngine;

public class InteractDialogueCommand : InteractCommand
{
    [SerializeField] private DialogueStarter _dialogueStarter;
    public override void Execute()
    {
        base.Execute();
        _dialogueStarter.Interact();
    }
}
