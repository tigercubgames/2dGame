using UnityEngine;

[RequireComponent(typeof(RouteMover))]
[RequireComponent(typeof(Follower))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(Flipper))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private EnemyState _currentState = EnemyState.Patrolling;
    private TargetDetector _targetDetector;
    private RouteMover _routeMover;
    private Follower _follower;
    private EnemyAttacker _attacker;
    private EnemyAnimator _animator;
    private Flipper _flipper;
    private Target _currentTarget;
    private Health _health;

    private enum EnemyState
    {
        Patrolling,
        Following,
        Attacking
    }

    private void Awake()
    {
        _routeMover = GetComponent<RouteMover>();
        _follower = GetComponent<Follower>();
        _targetDetector = GetComponentInChildren<TargetDetector>();
        _attacker = GetComponent<EnemyAttacker>();
        _animator = GetComponent<EnemyAnimator>();
        _flipper = GetComponent<Flipper>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _targetDetector.TargetDetected += OnTargetDetected;
        _targetDetector.TargetLost += OnTargetLost;
        _follower.TargetReached += OnTargetReached;
        _attacker.AttackStarted += OnAttackStarted;
        _attacker.AttackEnded += OnAttackEnded;
        _health.DamageTaken += OnDamageTaken;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _targetDetector.TargetDetected -= OnTargetDetected;
        _targetDetector.TargetLost -= OnTargetLost;
        _follower.TargetReached -= OnTargetReached;
        _attacker.AttackStarted -= OnAttackStarted;
        _attacker.AttackEnded -= OnAttackEnded;
        _health.DamageTaken -= OnDamageTaken;
        _health.Died -= OnDied;
    }
    
    private void Update()
    {
        UpdateFlip();
        
        if (_currentState == EnemyState.Following && _currentTarget != null)
        {
            if (_attacker.CanAttack(_currentTarget.transform.position))
            {
                TryAttack();
            }
        }
    }

    private void OnTargetDetected(Target target)
    {
        _currentTarget = target;
        SetState(EnemyState.Following);
    }

    private void OnTargetLost()
    {
        _currentTarget = null;
        SetState(EnemyState.Patrolling);
    }
    
    private void OnTargetReached()
    {
        TryAttack();
    }
    
    private void OnAttackStarted()
    {
        SetState(EnemyState.Attacking);
        _animator?.PlayAttackAnimation();
    }
    
    private void OnAttackEnded()
    {
        if (_currentTarget != null)
        {
            SetState(EnemyState.Following);
        }
        else
        {
            SetState(EnemyState.Patrolling);
        }
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
    
    private void TryAttack()
    {
        if (_currentTarget != null && _attacker.CanAttack(_currentTarget.transform.position))
        {
            _attacker.Attack();
        }
    }
    
    private void UpdateFlip()
    {
        Vector3 direction = Vector3.zero;
        
        if (_currentState == EnemyState.Patrolling)
        {
            direction = _routeMover.GetDirectionToWaypoint();
        }
        else if (_currentState == EnemyState.Following || _currentState == EnemyState.Attacking)
        {
            direction = _follower.GetDirectionToTarget();
        }
        
        if (direction.x != 0)
        {
            _flipper.Flip(direction.x);
        }
    }
    
    private void SetState(EnemyState newState)
    {
        if (_currentState == newState)
            return;

        _currentState = newState;

        switch (_currentState)
        {
            case EnemyState.Patrolling:
                _follower.StopFollowing();
                _routeMover.enabled = true;
                break;

            case EnemyState.Following:
                _routeMover.enabled = false;
                _follower.Follow(_currentTarget);
                break;
                
            case EnemyState.Attacking:
                _follower.StopFollowing();
                _routeMover.enabled = false;
                break;
        }
    }
}
