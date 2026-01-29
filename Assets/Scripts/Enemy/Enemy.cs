using UnityEngine;

[RequireComponent(typeof(RouteMover))]
[RequireComponent(typeof(Follower))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyStateMachineFactory))]
public class Enemy : MonoBehaviour
{
    private TargetDetector _targetDetector;
    private RouteMover _routeMover;
    private Follower _follower;
    private EnemyAttacker _attacker;
    private EnemyAnimator _animator;
    private Flipper _flipper;
    private Health _health;
    private StateMachine _stateMachine;

    private void Awake()
    {
        _routeMover = GetComponent<RouteMover>();
        _follower = GetComponent<Follower>();
        _targetDetector = GetComponentInChildren<TargetDetector>();
        _attacker = GetComponent<EnemyAttacker>();
        _animator = GetComponent<EnemyAnimator>();
        _flipper = GetComponent<Flipper>();
        _health = GetComponent<Health>();
        
        _stateMachine = GetComponent<EnemyStateMachineFactory>().Create
        (
            _routeMover,
            _follower,
            _targetDetector,
            _attacker,
            _flipper
        );
    }

    private void OnEnable()
    {
        _attacker.AttackStarted += OnAttackStarted;
        _health.DamageTaken += OnDamageTaken;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _attacker.AttackStarted -= OnAttackStarted;
        _health.DamageTaken -= OnDamageTaken;
        _health.Died -= OnDied;
    }

    private void Update()
    {
        _stateMachine?.Update();
        UpdateFlip();
    }

    private void OnAttackStarted()
    {
        _animator?.PlayAttackAnimation();
    }

    private void OnDamageTaken()
    {
        _animator.PlayHitAnimation();
    }

    private void OnDied()
    {
        Debug.Log($"{gameObject.name} погиб!");
        gameObject.SetActive(false);
    }

    private void UpdateFlip()
    {
        Vector3 direction = GetCurrentDirection();

        if (direction.x != 0)
        {
            _flipper.Flip(direction.x);
        }
    }

    private Vector3 GetCurrentDirection()
    {
        if (_targetDetector.HasTarget)
        {
            return _follower.GetDirectionToTarget();
        }

        return _routeMover.GetDirectionToWaypoint();
    }
}
