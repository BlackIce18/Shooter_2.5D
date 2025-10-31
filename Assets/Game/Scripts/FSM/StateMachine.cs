using UnityEngine;

public interface IState<T>
{
    void Enter(T owner);
    void Update(T owner);
    void Exit(T owner);
}
public class StateMachine<T>
{
    private readonly T _owner;
    private IState<T> _currentState;
    public IState<T> CurrentState => _currentState;

    public StateMachine(T owner)
    {
        _owner = owner;
    }

    public void ChangeState(IState<T> newState)
    {
        if (_currentState == newState) return;

        _currentState?.Exit(_owner);
        _currentState = newState;
        _currentState?.Enter(_owner);
    }

    public void Update()
    {
        _currentState?.Update(_owner);
    }
}
