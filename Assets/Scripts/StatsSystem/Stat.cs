using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class Stat : MonoBehaviour, IStat
{
    [SerializeField] private float _maxValue = 100f;
    [SerializeField] private float _minValue = 0f;
    
    private float _currentValue;
    
    public event Action<float, float> ValueChanged;
    
    public float CurrentValue => _currentValue;
    public float MaxValue => _maxValue;

    protected virtual void Awake()
    {
        _currentValue = _maxValue;
    }

    public void Add(float amount)
    {
        SetValue(_currentValue + amount);
    }
    
    public void Subtract(float amount)
    {
        SetValue(_currentValue - amount);
    }

    public void SetToMax()
    {
        SetValue(_maxValue);
    }

    public void SetToMin()
    {
        SetValue(_minValue);
    }

    private void SetValue(float newValue)
    {
        float clampedValue = Mathf.Clamp(newValue, 0, MaxValue);
        
        if(Mathf.Approximately(clampedValue, _currentValue))
            return;
        
        _currentValue = clampedValue;
        Debug.Log(_currentValue);
        ValueChanged?.Invoke(_currentValue, MaxValue);
    }
}
