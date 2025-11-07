using System.Windows.Input;
using UnityEngine;

public class KeyBindings : MonoBehaviour
{
    [SerializeField] private KeyCommand _dashCommand;
    [SerializeField] private KeyCommand inventoryUICommand;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _dashCommand.Execute();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUICommand.Execute();
        }
    }
}


