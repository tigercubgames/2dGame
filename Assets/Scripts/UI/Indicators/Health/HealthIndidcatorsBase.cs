using UnityEngine;

public abstract class HealthIndicatorBase : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected void Start()
    {
        ShowValue(Health.CurrentValue, Health.MaxValue);
    }

    protected void OnEnable()
    {
        Health.ValueChanged += ShowValue;
    }

    protected void OnDisable()
    {
        Health.ValueChanged -= ShowValue;
    }

    protected abstract void ShowValue(float currentHealth, float maxHealth);
}