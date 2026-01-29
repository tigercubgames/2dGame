using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;
    
    private float _currentHealth;

    public event Action<float, float> ValueChanged;
    public event Action Died;
    public event Action DamageTaken;
    public event Action Healed;

    public bool IsAlive => _currentHealth > 0;
    public float CurrentValue => _currentHealth;
    public float MaxValue => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Heal(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Отрицательное здоровье!");
        
        if (IsAlive == false)
            return;

        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        
        ValueChanged?.Invoke(_currentHealth, _maxHealth);
        Healed?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Отрицательный урон!");
        
        if (IsAlive == false)
            return;

        _currentHealth = Mathf.Max(_currentHealth - damage, 0f);
        
        ValueChanged?.Invoke(_currentHealth, _maxHealth);
        DamageTaken?.Invoke();

        if (IsAlive == false)
        {
            Died?.Invoke();
        }
    }
}