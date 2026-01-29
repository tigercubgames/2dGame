using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthText : HealthIndicatorBase
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override void ShowValue(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth:F0} / {maxHealth:F0}";
    }
}