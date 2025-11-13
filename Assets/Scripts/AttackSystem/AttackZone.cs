using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(TriggerDetector))]
public class AttackZone : MonoBehaviour
{
    private List<IDamageable> _targetsInZone = new List<IDamageable>();
    private TriggerDetector _triggerDetector;
    private IAttacker _attacker;
    
    private void Awake()
    {
        _triggerDetector = GetComponent<TriggerDetector>();
        _attacker = GetComponentInParent<IAttacker>();
    }

    private void OnEnable()
    {
        _triggerDetector.TriggerEntered += OnZoneEntered;
        _triggerDetector.TriggerExited += OnZoneExited;
    }

    private void OnDisable()
    {
        _triggerDetector.TriggerEntered -= OnZoneEntered;
        _triggerDetector.TriggerExited -= OnZoneExited;
    }
    
    public void ApplyDamage()
    {
        List<IDamageable> targetsCopy = new List<IDamageable>(_targetsInZone);
        
        foreach (IDamageable target in targetsCopy)
        {
            target.TakeDamage(_attacker.Damage);
        }
    }
    
    private void OnZoneEntered(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if (_targetsInZone.Contains(damageable) == false)
            {
                _targetsInZone.Add(damageable);
            }
        }
    }

    private void OnZoneExited(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            _targetsInZone.Remove(damageable);
        }
    }
}
