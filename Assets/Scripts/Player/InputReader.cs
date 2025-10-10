using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode JumpKey = KeyCode.Space;
    
    public event Action<float> MovePerformed;
    public event Action MoveCanceled;
    public event Action JumpPerformed;

    private float _previousMoveInput;

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
    }

    private void HandleMovementInput()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        
        if (moveInput != _previousMoveInput)
        {
            if (moveInput != 0)
            {
                MovePerformed?.Invoke(moveInput);
            }
            else
            {
                MoveCanceled?.Invoke();
            }
            
            _previousMoveInput = moveInput;
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(JumpKey))
        {
            JumpPerformed?.Invoke();
        }
    }
}
