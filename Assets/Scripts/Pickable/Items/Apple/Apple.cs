using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : PickableItem
{
    [SerializeField] private float _healAmount = 20f;
    
    protected override void ApplyEffect(GameObject collector)
    {
        if (collector.TryGetComponent(out Health health))
        {
            health.Heal(_healAmount);
            Debug.Log($"Восстановлено {_healAmount} здоровья!");
        }
    }
}
