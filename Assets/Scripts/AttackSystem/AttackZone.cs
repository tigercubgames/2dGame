using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackZone : MonoBehaviour
{
    private List<IDamageable> _targetsInZone = new List<IDamageable>();
    private Collider2D _collider;
    private IAttacker _attacker;
    
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _attacker = GetComponentInParent<IAttacker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (_targetsInZone.Contains(damageable) == false)
            {
                _targetsInZone.Add(damageable);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            _targetsInZone.Remove(damageable);
        }
    }
    
    public void ApplyDamage()
    {
        List<IDamageable> targetsCopy = new List<IDamageable>(_targetsInZone);
        
        foreach (IDamageable target in targetsCopy)
        {
            target.TakeDamage(_attacker.Damage);
        }
    }
}