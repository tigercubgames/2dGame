using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundDetector))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    
    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;

    public bool IsGrounded => _groundDetector.IsGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    public void Move(float direction)
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = direction * _moveSpeed;
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