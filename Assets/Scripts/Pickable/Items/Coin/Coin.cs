using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : PickableItem
{
    public override void ApplyPickUpEffect(Collider2D toucher)
    {
        Debug.Log($"{toucher.name} собрал монетку!");
    }
}
