using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string  IsRunning = "IsRunning";
    private const string  IsJumping = "IsJumping";
    
    private Animator _animator;
    private Flipper _flipper;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _flipper = GetComponent<Flipper>();
    }
    
    public void UpdateAnimations(float moveInput, bool isGrounded)
    {
        bool isRunning = Mathf.Abs(moveInput) > 0.1f;
        _animator.SetBool(IsRunning, isRunning);

        if (isRunning)
        {
            _flipper.HandleMoveInput(moveInput);
        }

        _animator.SetBool(IsJumping, !isGrounded);
    }
}
