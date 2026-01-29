using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : HealthIndicatorBase
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void ShowValue(float currentHealth, float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = currentHealth;
    }
}