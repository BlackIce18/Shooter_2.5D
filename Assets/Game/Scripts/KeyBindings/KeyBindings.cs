using System.Windows.Input;
using UnityEngine;

public class KeyBindings : MonoBehaviour
{
    [SerializeField] private KeyCommand _dashCommand;
    [SerializeField] private KeyCommand _inventoryUICommand;
    [SerializeField] private KeyCommand _interact;
    [SerializeField] private KeyCommand _characteristicsUICommand;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _dashCommand.Execute();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _inventoryUICommand.Execute();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _interact?.Execute();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _characteristicsUICommand.Execute();
        }
    }
    public void BindInteract(KeyCommand keyCommand)
    {
        _interact = keyCommand;
    }
}


