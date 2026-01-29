public class PatrolState : State
{
    private readonly RouteMover _routeMover;

    public PatrolState(IStateChanger stateChanger, RouteMover routeMover) : base(stateChanger)
    {
        _routeMover = routeMover;
    }

    protected override void OnEnter()
    {
        _routeMover.enabled = true;
    }

    protected override void OnExit()
    {
        _routeMover.enabled = false;
    }
}
