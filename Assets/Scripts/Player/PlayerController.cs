using UnityEngine;

[RequireComponent(typeof(MoveHandler), typeof(GroundDetector))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    
    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;
    private float _moveInput;

    public float MoveInput => _moveInput;
    public bool IsGrounded => _groundDetector.IsGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    public void SetMoveInput(float moveInput)
    {
        _moveInput = moveInput;
    }

    private void FixedUpdate()
    {
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
        if (IsGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }
}
