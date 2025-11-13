using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyState _currentState = EnemyState.Patrolling;
    private TargetDetector _targetDetector;
    private RouteMover _routeMover;
    private Follower _follower;
    private EnemyAttacker _attacker;
    private Target _currentTarget;

    private enum EnemyState
    {
        Patrolling,
        Following
    }

    private void Awake()
    {
        _routeMover = GetComponent<RouteMover>();
        _follower = GetComponent<Follower>();
        _targetDetector = GetComponentInChildren<TargetDetector>();
        _attacker = GetComponent<EnemyAttacker>();
    }

    private void OnEnable()
    {
        _targetDetector.TargetDetected += OnTargetDetected;
        _targetDetector.TargetLost += OnTargetLost;
        _follower.TargetReached += TryAttackTarget;
        _attacker.AttackEnded += TryAttackTarget;
    }

    private void OnDisable()
    {
        _targetDetector.TargetDetected -= OnTargetDetected;
        _targetDetector.TargetLost -= OnTargetLost;
        _follower.TargetReached -= TryAttackTarget;
        _attacker.AttackEnded -= TryAttackTarget;
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
    
    private void TryAttackTarget()
    {
        if (_currentTarget != null && _attacker != null)
        {
            if (_attacker.CanAttack(_currentTarget.transform.position))
            {
                _attacker.TryAttack();
            }
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
                _follower.StopFollow();
                _routeMover.enabled = true;
                break;

            case EnemyState.Following:
                _routeMover.enabled = false;
                _follower.Follow(_currentTarget);
                break;
        }
    }
}
