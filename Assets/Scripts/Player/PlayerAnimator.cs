using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string  IsRunning = "IsRunning";
    private const string  IsJumping = "IsJumping";
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void UpdateAnimations(float moveInput, bool isGrounded)
    {
        bool isRunning = Mathf.Abs(moveInput) > 0.1f;
        _animator.SetBool(IsRunning, isRunning);

        if (isRunning)
        {
            _spriteRenderer.flipX = moveInput < 0;
        }

        _animator.SetBool(IsJumping, !isGrounded);
    }
}
