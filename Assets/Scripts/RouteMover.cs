using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RouteMover : MonoBehaviour
{
    [SerializeField] private Transform _route;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _waypointReachDistance = 0.5f;

    private Transform[] _waypoints;
    private Transform _currentWaypoint;
    private Flipper _flipper;
    private int _currentWaypointIndex = 0;

    private void Awake()
    {
        _flipper = GetComponent<Flipper>();
        
        InitializeWaypoints();
        _currentWaypoint = _waypoints[_currentWaypointIndex];
    }

    private void Start()
    {
        transform.position = _currentWaypoint.position;
    }

    private void Update()
    {
        if (IsWaypointReached())
        {
            SelectNextWaypoint();
        }
        
        MoveToWaypoint();
        UpdateDirection();
    }
    
    private bool IsWaypointReached()
    {
        float sqrDistance = (_currentWaypoint.position - transform.position).sqrMagnitude;
        float sqrThreshold = _waypointReachDistance * _waypointReachDistance;
        
        return sqrDistance <= sqrThreshold;
    }

    private void InitializeWaypoints()
    {
        _waypoints = new Transform[_route.childCount];

        for (int i = 0; i < _route.childCount; i++)
        {
            _waypoints[i] = _route.GetChild(i);
        }
    }

    private void MoveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, _speed * Time.deltaTime);
    }

    private void SelectNextWaypoint()
    {
        _currentWaypointIndex++;
        _currentWaypointIndex %= _waypoints.Length;
        _currentWaypoint = _waypoints[_currentWaypointIndex];
    }
    
    private void UpdateDirection()
    {
        float directionX = _currentWaypoint.position.x - transform.position.x;
        _flipper.HandleMoveInput(directionX);
    }
}
