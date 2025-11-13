using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode JumpKey = KeyCode.Space;
    private const KeyCode AttackKey = KeyCode.LeftShift;
    
    public event Action<float> MovePerformed;
    public event Action MoveCanceled;
    public event Action JumpPerformed;
    public event Action AttackPerformed;

    private float _previousMoveInput;

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleAttackInput();
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
    
    private void HandleAttackInput()
    {
        if (Input.GetKeyDown(AttackKey))
        {
            AttackPerformed?.Invoke();
        }
    }
}
