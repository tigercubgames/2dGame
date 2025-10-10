using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : PickableItem
{
    public override void ApplyPickUpEffect()
    {
        Debug.Log($"Яблоко собрано!");
    }
}
