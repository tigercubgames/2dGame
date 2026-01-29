using UnityEngine;

public class AttackState : State
{
    private readonly EnemyAttacker _attacker;
    private readonly Flipper _flipper;
    private readonly Follower _follower;

    public AttackState
    (
        IStateChanger stateChanger,
        EnemyAttacker attacker,
        Flipper flipper,
        Follower follower
    ) : base(stateChanger)
    {
        _attacker = attacker;
        _flipper = flipper;
        _follower = follower;
    }

    protected override void OnEnter()
    {
        _attacker.Attack();
    }

    protected override void OnUpdate()
    {
        Vector3 direction = _follower.GetDirectionToTarget();

        if (direction.x != 0)
        {
            _flipper.Flip(direction.x);
        }
    }
}
