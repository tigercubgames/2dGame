using UnityEngine;

[RequireComponent(typeof(MoveHandler))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private float _moveInput;

    public float MoveInput => _moveInput;
    public bool IsGrounded => _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetMoveInput(float moveInput)
    {
        _moveInput = moveInput;
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }

    private void Move()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _moveInput * _moveSpeed;
        _rigidbody.velocity = velocity;
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _groundLayer);
    }
}
