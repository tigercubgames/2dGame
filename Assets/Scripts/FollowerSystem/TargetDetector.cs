using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerDetector))]
public class TargetDetector : MonoBehaviour
{
    private TriggerDetector _triggerDetector;
    private Target _currentTarget;
    
    public event Action<Target> TargetDetected;
    public event Action TargetLost;

    private void Awake()
    {
        _triggerDetector = GetComponent<TriggerDetector>();
    }

    private void OnEnable()
    {
        _triggerDetector.TriggerEntered += OnEntered;
        _triggerDetector.TriggerExited += OnExited;
    }

    private void OnDisable()
    {
        _triggerDetector.TriggerEntered -= OnEntered;
        _triggerDetector.TriggerExited -= OnExited;
    }

    private void OnEntered(Collider2D other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
            _currentTarget = target;
            TargetDetected?.Invoke(_currentTarget);
        }
    }

    private void OnExited(Collider2D other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
            _currentTarget = null;
            TargetLost?.Invoke();
        }
    }
}
