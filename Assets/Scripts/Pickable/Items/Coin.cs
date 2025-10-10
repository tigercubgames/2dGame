using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : PickableItem
{
    public override void ApplyPickUpEffect()
    {
        Debug.Log($"Монетка собрана!");
    }
}
