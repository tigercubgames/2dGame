using System;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _stopDistance = 0.5f;
    
    private Target _target;
    private bool _isFollowing = false;
    private bool _hasReachedTarget = false;
    
    public event Action TargetReached;

    private void Update()
    {
        if (_isFollowing && _target != null)
        {
            UpdateFollowing();
        }
    }

    public void Follow(Target target)
    {
        _target = target;
        _isFollowing = true;
        _hasReachedTarget = false;
    }

    public void StopFollowing()
    {
        _target = null;
        _isFollowing = false;
        _hasReachedTarget = false;
    }
    
    public Vector3 GetDirectionToTarget()
    {
        if (_target != null)
        {
            return (_target.transform.position - transform.position).normalized;
        }
        
        return Vector3.zero;
    }
    
    private void UpdateFollowing()
    {
        if (IsCloseEnoughToTarget())
        {
            OnTargetReached();
        }
        else
        {
            MoveTowardsTarget();
        }
    }
    
    private bool IsCloseEnoughToTarget()
    {
        float sqrDistance = (_target.transform.position - transform.position).sqrMagnitude;
        float sqrStopDistance = _stopDistance * _stopDistance;
        
        return sqrDistance <= sqrStopDistance;
    }
    
    private void OnTargetReached()
    {
        if (_hasReachedTarget == false)
        {
            _hasReachedTarget = true;
            TargetReached?.Invoke();
        }
    }
    
    private void MoveTowardsTarget()
    {
        _hasReachedTarget = false;
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            _target.transform.position, 
            _speed * Time.deltaTime
        );
    }
}