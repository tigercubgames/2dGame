using System;
using System.Collections.Generic;

public abstract class State
{
    private readonly IStateChanger _stateChanger;
    private readonly List<Transition> _transitions = new List<Transition>();

    protected State(IStateChanger stateChanger)
    {
        _stateChanger = stateChanger;
    }

    public void AddTransition(Transition transition)
    {
        if (transition == null)
            throw new ArgumentNullException(nameof(transition));

        if (_transitions.Contains(transition))
            throw new ArgumentException($"Transition already exists: {transition.GetType().Name}");

        _transitions.Add(transition);
    }

    public void Enter()
    {
        OnEnter();
    }

    public void Exit()
    {
        OnExit();
    }

    public void Update()
    {
        foreach (Transition transition in _transitions)
        {
            if (transition.CanTransit())
            {
                _stateChanger.ChangeState(transition.NextState);
                return;
            }
        }

        OnUpdate();
    }

    protected virtual void OnEnter() { }

    protected virtual void OnExit() { }

    protected virtual void OnUpdate() { }
}