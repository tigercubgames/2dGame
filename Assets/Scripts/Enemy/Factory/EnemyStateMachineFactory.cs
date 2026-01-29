using UnityEngine;

public class EnemyStateMachineFactory : MonoBehaviour
{
    public StateMachine Create
    (
        RouteMover routeMover,
        Follower follower,
        TargetDetector targetDetector,
        EnemyAttacker attacker,
        Flipper flipper
    )
    {
        StateMachine stateMachine = new StateMachine();

        PatrolState patrolState = new PatrolState(stateMachine, routeMover);
        ChaseState chaseState = new ChaseState(stateMachine, follower, targetDetector);
        AttackState attackState = new AttackState(stateMachine, attacker, flipper, follower);

        ToPatrolStateTransition toPatrolStateTransition = new ToPatrolStateTransition(patrolState, targetDetector);
        ToChaseStateTransition toChaseStateTransition = new ToChaseStateTransition(chaseState, targetDetector, attacker);
        ToAttackStateTransition toAttackStateTransition = new ToAttackStateTransition(attackState, targetDetector, attacker);

        patrolState.AddTransition(toChaseStateTransition);
            
        chaseState.AddTransition(toAttackStateTransition);
        chaseState.AddTransition(toPatrolStateTransition);
            
        attackState.AddTransition(toChaseStateTransition);
        attackState.AddTransition(toPatrolStateTransition);

        stateMachine.ChangeState(patrolState);

        return stateMachine;
    }
}
