using UnityEngine;

public class RouteMover : MonoBehaviour
{
    [SerializeField] private Transform _route;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _waypointReachDistance = 0.5f;

    private Transform _currentWaypoint;
    private int _currentWaypointIndex = 0;

    private void Awake()
    {
        if (_waypoints.Length > 0)
        {
            _currentWaypoint = _waypoints[_currentWaypointIndex];
        }
    }

    private void Start()
    {
        if (_currentWaypoint != null)
        {
            transform.position = _currentWaypoint.position;
        }
    }

    private void Update()
    {
        if (_waypoints.Length == 0)
            return;
        
        if (IsWaypointReached())
        {
            SelectNextWaypoint();
        }
        
        MoveToWaypoint();
    }
    
    public Vector3 GetDirectionToWaypoint()
    {
        if (_currentWaypoint != null)
        {
            return (_currentWaypoint.position - transform.position).normalized;
        }
        
        return Vector3.zero;
    }
    
    private bool IsWaypointReached()
    {
        float sqrDistance = (_currentWaypoint.position - transform.position).sqrMagnitude;
        float sqrThreshold = _waypointReachDistance * _waypointReachDistance;
        
        return sqrDistance <= sqrThreshold;
    }

    private void MoveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            _currentWaypoint.position, 
            _speed * Time.deltaTime
        );
    }

    private void SelectNextWaypoint()
    {
        _currentWaypointIndex++;
        _currentWaypointIndex %= _waypoints.Length;
        _currentWaypoint = _waypoints[_currentWaypointIndex];
    }
    
#if UNITY_EDITOR
    [ContextMenu("Fill Waypoints From Route")]
    private void FillWaypointsFromRoute()
    {
        if (_route == null)
        {
            Debug.LogWarning("Route is not assigned!");
            return;
        }
        
        int waypointCount = _route.childCount;
        _waypoints = new Transform[waypointCount];

        for (int i = 0; i < waypointCount; i++)
        {
            _waypoints[i] = _route.GetChild(i);
        }
        
        Debug.Log($"Filled {waypointCount} waypoints from route.");
    }
#endif
}
