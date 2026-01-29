public class ToPatrolStateTransition : Transition
{
    private readonly TargetDetector _targetDetector;

    public ToPatrolStateTransition(State nextState, TargetDetector targetDetector) : base(nextState)
    {
        _targetDetector = targetDetector;
    }

    public override bool CanTransit()
    {
        return _targetDetector.HasTarget == false;
    }
}
