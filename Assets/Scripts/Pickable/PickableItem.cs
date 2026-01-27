using System;
using UnityEngine;

public abstract class PickableItem : MonoBehaviour
{
    public event Action<PickableItem> PickedUp;
    
    public void PickUp(GameObject collector)
    {
        ApplyEffect(collector);
        PickedUp?.Invoke(this);
    }
    
    protected abstract void ApplyEffect(GameObject collector);
}
