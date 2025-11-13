using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth : IStat
{
    bool IsAlive { get; }
    
    void TakeDamage(float damage);
    void Heal(float amount);
}
