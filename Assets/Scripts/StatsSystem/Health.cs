using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Stat, IHealth, IDamageable
{
    public bool IsAlive => CurrentValue > 0;
    
    private HitEffect _hitEffect;

    protected override void Awake()
    {
        base.Awake();
        _hitEffect = GetComponent<HitEffect>();
    }
    
    private void OnEnable()
    {
        ValueChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        ValueChanged -= OnHealthChanged;
    }

    public void TakeDamage(float damage)
    {
        if(IsAlive == false)
            return;
        
        Subtract(damage);
        _hitEffect?.PlayHitEffect();
    }

    public void Heal(float amount)
    {
        if(IsAlive == false)
            return;
        
        Add(amount);
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        if (currentHealth <= 0)
        {
            Debug.Log($"{this.name} погиб!");
            this.gameObject.SetActive(false);
        }
    }
}
