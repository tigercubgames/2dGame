using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : PickableItem
{
    protected override void ApplyEffect(GameObject collector)
    {
        Debug.Log($"{collector.name} собрал монетку!");
    }
}
