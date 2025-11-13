using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerDetector : MonoBehaviour
{
    public event Action<Collider2D> TriggerEntered;
    public event Action<Collider2D> TriggerExited;
    
    private Collider2D _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider2D>();
        _trigger.isTrigger = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEntered?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TriggerExited?.Invoke(other);
    }
}
