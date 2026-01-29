public class ToAttackStateTransition : Transition
{
    private readonly TargetDetector _targetDetector;
    private readonly EnemyAttacker _attacker;

    public ToAttackStateTransition
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
        if (_targetDetector.HasTarget == false)
            return false;

        return _attacker.CanAttack(_targetDetector.CurrentTarget.transform.position);
    }
}
