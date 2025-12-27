using UnityEngine;

public class InteractCommand : KeyCommand
{
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private GameObject _interactUI;
    [SerializeField] private DialogueStarter _dialogueStarter;
    public override void Execute()
    {
        _interactUI.gameObject.SetActive(false);
        //_dialogueUI.gameObject.SetActive(true);
        _dialogueStarter.Interact();
        //Проверить, что мы находимся в зоне взаимодействия 
    }
}
