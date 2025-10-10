using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TouchDetector : MonoBehaviour
{
    public event Action<Collider2D> Touched;
    
    private Collider2D _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider2D>();
        _trigger.isTrigger = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Touched?.Invoke(other);
    }
}
