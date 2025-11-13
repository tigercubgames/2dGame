using System;
using UnityEngine;

public abstract class PickableItem : MonoBehaviour
{
    public event Action<PickableItem> Picked;
    
    private IPickablePool _pool;
    
    public abstract void ApplyPickUpEffect(Collider2D toucher);
    
    public void PickUp(Collider2D collider)
    {
        ApplyPickUpEffect(collider);
        Picked?.Invoke(this);
        _pool?.ReturnItem(this);
    }
    
    public void SetPool(IPickablePool pool)
    {
        _pool = pool;
    }
}
