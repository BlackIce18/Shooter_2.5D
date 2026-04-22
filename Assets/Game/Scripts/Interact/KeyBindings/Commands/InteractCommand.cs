using UnityEngine;

public class InteractCommand : KeyCommand
{
    [SerializeField] private GameObject _interactUI;
    public override void Execute()
    {
        _interactUI.gameObject.SetActive(false);
        //Проверить, что мы находимся в зоне взаимодействия 
    }
}
