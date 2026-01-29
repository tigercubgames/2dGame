using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;
    private const KeyCode AttackKey = KeyCode.LeftShift;
    private const KeyCode VampirismKey = KeyCode.E;
    
    public event Action<float> MovePerformed;
    public event Action MoveCanceled;
    public event Action JumpPerformed;
    public event Action AttackPerformed;
    public event Action VampirismPerformed;

    private float _previousMoveInput;

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleAttackInput();
        HandleVampirismInput();
    }

    private void HandleMovementInput()
    {
        float moveInput = Input.GetAxisRaw(HorizontalAxis);
        
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
    
    private void HandleVampirismInput()
    {
        if (Input.GetKeyDown(VampirismKey))
        {
            VampirismPerformed?.Invoke();
        }
    }
}