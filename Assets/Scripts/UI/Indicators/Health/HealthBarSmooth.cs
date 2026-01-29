using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarSmooth : HealthIndicatorBase
{
    [SerializeField] private float _smoothSpeed = 1f;

    private Slider _slider;
    private Coroutine _coroutine;
    private bool _isInitialized = false;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void ShowValue(float currentHealth, float maxHealth)
    {
        _slider.maxValue = maxHealth;
        
        if (_isInitialized == false)
        {
            _slider.value = currentHealth;
            _isInitialized = true;
            return;
        }
        
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValueSmoothly(currentHealth));
    }

    private IEnumerator ChangeValueSmoothly(float targetHealth)
    {
        while (Mathf.Approximately(_slider.value, targetHealth) == false) 
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetHealth, _smoothSpeed * Time.deltaTime);
            yield return null;
        }
        
        _slider.value = targetHealth;
    }
}