using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    private bool _isFacingRight = true;

    public void HandleMoveInput(float direction)
    {
        if (direction > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (direction < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        
        Vector3 rotation = transform.eulerAngles;
        
        if (_isFacingRight)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }
        
        transform.eulerAngles = rotation;
    }
}
