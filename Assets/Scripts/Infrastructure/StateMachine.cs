public class StateMachine : IStateChanger
{
    private State _currentState;

    public void Update()
    {
        if (_currentState == null)
            return;

        _currentState.Update();
    }

    public void ChangeState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
