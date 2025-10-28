using System.Windows.Input;
using UnityEngine;

public class KeyBindings : MonoBehaviour
{
    [SerializeField] private DashCommand _dashCommand;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _dashCommand.Execute();
        }
    }
}


