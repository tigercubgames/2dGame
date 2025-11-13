using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStat
{
    float CurrentValue { get; }
    float MaxValue { get; }
    event Action<float, float> ValueChanged;
    
    void Add(float amount);
    void Subtract(float amount);
    void SetToMax();
    void SetToMin();
}
