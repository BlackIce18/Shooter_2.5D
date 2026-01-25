using UnityEngine;

public class WindowUIDoubleCommand : WindowUICommand
{
    [SerializeField] private KeyCommand _keyCommand;
    
    public override void Execute()
    {
        base.Execute();
        _keyCommand.Execute();
    }
    
    
}
