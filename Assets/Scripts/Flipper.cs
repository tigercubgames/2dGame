using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    private bool _isFacingRight = true;

    public void Flip(float direction)
    {
        if (direction > 0 && !_isFacingRight)
        {
            FlipSprite();
        }
        else if (direction < 0 && _isFacingRight)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        _isFacingRight = !_isFacingRight;
        
        Vector3 rotation = transform.eulerAngles;
        
        rotation.y = _isFacingRight ? 0f : 180f;
        
        transform.eulerAngles = rotation;
    }
}
