using System;
using UnityEngine;

public abstract class PickableItem : MonoBehaviour
{
    public event Action<PickableItem> Picked;
    
    private IPickablePool _pool;
    
    public abstract void ApplyPickUpEffect();
    
    public void PickUp()
    {
        ApplyPickUpEffect();
        Picked?.Invoke(this);
        _pool?.ReturnItem(this);
    }
    
    public void SetPool(IPickablePool pool)
    {
        _pool = pool;
    }
}
