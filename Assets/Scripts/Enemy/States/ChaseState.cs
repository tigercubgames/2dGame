using System;

public class ChaseState : State, IDisposable
{
    private readonly Follower _follower;
    private readonly TargetDetector _targetDetector;

    private Target _currentTarget;

    public ChaseState
    (
        IStateChanger stateChanger,
        Follower follower,
        TargetDetector targetDetector
    ) : base(stateChanger)
    {
        _follower = follower;
        _targetDetector = targetDetector;

        _targetDetector.TargetDetected += OnTargetDetected;
        _targetDetector.TargetLost += OnTargetLost;
    }

    public void Dispose()
    {
        _targetDetector.TargetDetected -= OnTargetDetected;
        _targetDetector.TargetLost -= OnTargetLost;
    }

    protected override void OnEnter()
    {
        if (_currentTarget != null)
        {
            _follower.Follow(_currentTarget);
        }
    }

    protected override void OnExit()
    {
        _follower.StopFollowing();
    }

    private void OnTargetDetected(Target target)
    {
        _currentTarget = target;
    }

    private void OnTargetLost()
    {
        _currentTarget = null;
    }
}
