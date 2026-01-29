public class ToChaseStateTransition : Transition
{
    private readonly TargetDetector _targetDetector;
    private readonly EnemyAttacker _attacker;

    public ToChaseStateTransition
    (
        State nextState,
        TargetDetector targetDetector,
        EnemyAttacker attacker
    ) : base(nextState)
    {
        _targetDetector = targetDetector;
        _attacker = attacker;
    }

    public override bool CanTransit()
    {
        return _targetDetector.HasTarget && _attacker.CanAttack(_targetDetector.CurrentTarget.transform.position) == false;
    }
}
