using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchDetector))]
public class PickupOnTouchHandler : MonoBehaviour
{
    private TouchDetector _touchDetector;
    private PickableItem _pickableItem;
    
    private void Awake()
    {
        _touchDetector = GetComponent<TouchDetector>();
        _pickableItem  = GetComponent<PickableItem>();
    }
    
    private void OnEnable()
    {
        _touchDetector.Touched += OnTouched;
    }

    private void OnDisable()
    {
        _touchDetector.Touched -= OnTouched;
    }
    
    private void OnTouched(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _pickableItem?.PickUp(other);
        }
    }
}
