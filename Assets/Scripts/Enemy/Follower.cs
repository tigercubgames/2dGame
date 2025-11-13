using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _targetReachDistance = 0.5f;
    
    private Target _target;
    private Flipper _flipper;
    private EnemyAttacker _attacker;
    private bool _isFollowing = false;
    
    public event Action TargetReached;

    private void Awake()
    {
        _flipper = GetComponent<Flipper>();
        _attacker = GetComponent<EnemyAttacker>();
    }

    private void Update()
    {
        if (_isFollowing && _target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);

            if (_attacker != null && distanceToTarget <= _attacker.AttackRange)
            {
                TargetReached?.Invoke();
            }
            else if (distanceToTarget > _targetReachDistance)
            {
                FollowTarget();
                UpdateDirection();
            }
        }
    }

    public void Follow(Target target)
    {
        _target = target;
        _isFollowing = true;
    }

    public void StopFollow()
    {
        _target = null;
        _isFollowing = false;
    }

    private void FollowTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }

    private void UpdateDirection()
    {
        float directionX = _target.transform.position.x - transform.position.x;
        _flipper?.HandleMoveInput(directionX);
    }
}
