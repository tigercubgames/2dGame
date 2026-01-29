public abstract class Transition
{
    public State NextState { get; private set; }

    protected Transition(State nextState)
    {
        NextState = nextState;
    }

    public abstract bool CanTransit();
}