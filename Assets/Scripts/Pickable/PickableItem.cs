using System;
using UnityEngine;

public abstract class PickableItem : MonoBehaviour
{
    public event Action<PickableItem> Picked;
    
    private PickablePool _pool;
    
    public abstract void ApplyPickUpEffect();
    public void PickUp()
    {
        ApplyPickUpEffect();
        Picked?.Invoke(this);
        _pool.ReturnItem(this);
    }
    
    public void SetPool(PickablePool pool)
    {
        _pool = pool;
    }
}
