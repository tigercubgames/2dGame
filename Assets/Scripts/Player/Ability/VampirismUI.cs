using UnityEngine;
using UnityEngine.UI;

public class VampirismUI : MonoBehaviour
{
    [SerializeField] private Vampirism _ability;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Color _activeColor = Color.white;
    [SerializeField] private Color _cooldownColor = Color.blue;
    
    private void Awake()
    {
        if (_slider != null)
        {
            _slider.minValue = 0;
            _slider.maxValue = 1;
            _slider.value = 1;
        }
        
        if (_fillImage != null)
            _fillImage.color = _activeColor;
    }

    private void OnEnable()
    {
        _ability.DurationChanged += OnDurationChanged;
        _ability.CooldownChanged += OnCooldownChanged;
        _ability.AbilityActivated += OnAbilityActivated;
        _ability.AbilityDeactivated += OnAbilityDeactivated;
        _ability.CooldownFinished += OnCooldownFinished;
    }

    private void OnDisable()
    {
        _ability.DurationChanged -= OnDurationChanged;
        _ability.CooldownChanged -= OnCooldownChanged;
        _ability.AbilityActivated -= OnAbilityActivated;
        _ability.AbilityDeactivated -= OnAbilityDeactivated;
        _ability.CooldownFinished -= OnCooldownFinished;
    }

    private void OnAbilityActivated()
    {
        if (_fillImage != null)
            _fillImage.color = _activeColor;
        
        if (_slider != null)
            _slider.value = 1; 
    }

    private void OnAbilityDeactivated()
    {
        if (_fillImage != null)
            _fillImage.color = _cooldownColor;
        
        if (_slider != null)
            _slider.value = 0;
    }

    private void OnCooldownFinished()
    {
        if (_fillImage != null)
            _fillImage.color = _activeColor;
        
        if (_slider != null)
            _slider.value = 1;
    }

    private void OnDurationChanged(float current, float max)
    {
        if (_slider != null)
            _slider.value = current / max;
    }

    private void OnCooldownChanged(float current, float max)
    {
        if (_slider != null)
            _slider.value = (max - current) / max;
    }
}
