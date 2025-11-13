using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : PickableItem
{
    [SerializeField] private float _healAmount = 20f;
    
    public override void ApplyPickUpEffect(Collider2D toucher)
    {
        if (toucher.TryGetComponent<IHealth>(out IHealth health))
        {
            health.Heal(_healAmount);
            Debug.Log($"Восстановлено {_healAmount} здоровья!");
        }
    }
}
