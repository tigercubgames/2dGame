using System;
using UnityEngine;

[RequireComponent(typeof(OverlapDetector))]
public class TargetDetector : MonoBehaviour
{
    private OverlapDetector _detector;
    private Target _currentTarget;
    
    public event Action<Target> TargetDetected;
    public event Action TargetLost;

    public bool HasTarget => _currentTarget != null;
    public Target CurrentTarget => _currentTarget;

    private void Awake()
    {
        _detector = GetComponent<OverlapDetector>();
    }

    private void OnEnable()
    {
        _detector.Entered += OnEntered;
        _detector.Exited += OnExited;
    }

    private void OnDisable()
    {
        _detector.Entered -= OnEntered;
        _detector.Exited -= OnExited;
    }

    private void OnEntered(Collider2D other)
    {
        if (other.TryGetComponent(out Target target))
        {
            _currentTarget = target;
            TargetDetected?.Invoke(_currentTarget);
        }
    }

    private void OnExited(Collider2D other)
    {
        if (other.TryGetComponent(out Target target))
        {
            _currentTarget = null;
            TargetLost?.Invoke();
        }
    }
}