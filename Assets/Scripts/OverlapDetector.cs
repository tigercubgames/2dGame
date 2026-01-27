using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetector : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private LayerMask _detectionLayer;
    [SerializeField] private float _checkRate = 0.1f;
    [SerializeField] private int _maxColliders = 3;
    
    private Collider2D[] _overlapBuffer;
    private List<Collider2D> _previousDetected = new List<Collider2D>();
    private List<Collider2D> _currentDetected = new List<Collider2D>();
    private WaitForSeconds _checkDelay;
    
    public event Action<Collider2D> Entered;
    public event Action<Collider2D> Exited;
    
    private void Awake()
    {
        _overlapBuffer = new Collider2D[_maxColliders];
        _checkDelay = new WaitForSeconds(_checkRate);
    }
    
    private void OnEnable()
    {
        StartCoroutine(Detection());
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
        _previousDetected.Clear();
        _currentDetected.Clear();
    }
    
    private IEnumerator Detection()
    {
        while (enabled)
        {
            DetectOverlaps();
            yield return _checkDelay;
        }
    }
    
    private void DetectOverlaps()
    {
        _currentDetected.Clear();
        
        int overlapCount = Physics2D.OverlapCircleNonAlloc(
            transform.position, 
            _detectionRadius, 
            _overlapBuffer, 
            _detectionLayer
        );
        
        for (int i = 0; i < overlapCount; i++)
        {
            Collider2D overlap = _overlapBuffer[i];
            
            if (overlap == null || overlap.gameObject == gameObject)
                continue;
            
            _currentDetected.Add(overlap);
            
            if (_previousDetected.Contains(overlap) == false)
            {
                Entered?.Invoke(overlap);
            }
        }
        
        for (int i = _previousDetected.Count - 1; i >= 0; i--)
        {
            if (_currentDetected.Contains(_previousDetected[i]) == false)
            {
                Exited?.Invoke(_previousDetected[i]);
            }
        }
        
        _previousDetected.Clear();
        _previousDetected.AddRange(_currentDetected);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
