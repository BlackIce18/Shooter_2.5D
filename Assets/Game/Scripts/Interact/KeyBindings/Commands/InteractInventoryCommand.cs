using UnityEngine;

public class InteractInventroryCommand : KeyCommand
{
    [SerializeField] private GameObject _interactUI;
    [SerializeField] private WindowUICommand _windowUICommand;
    [SerializeField] private WindowUICommand _inventory;
    public override void Execute()
    {
        _windowUICommand.Execute();
        _inventory.Execute();
        _interactUI.gameObject.SetActive(false);
        //Проверить, что мы находимся в зоне взаимодействия 
    }
}
