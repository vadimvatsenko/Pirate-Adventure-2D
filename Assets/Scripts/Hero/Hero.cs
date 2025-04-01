using System;
using UnityEngine;
public class Hero : MonoBehaviour
{
    #region Static Fields
    // что тут происходит, перевод string в hash
    private static readonly int XVelocityKey = Animator.StringToHash("xVelocity");
    private static readonly int YVelocityKey = Animator.StringToHash("yVelocity");
    private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
    #endregion
    
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    
    [Header("Collision Info")]
    private bool _isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector3 groundCheckDistance;
    [SerializeField] private float groundCheckRadius;
    
    [Header("Buffer Jump")] 
    [SerializeField] private float jumpBufferWindow;
    private float _bufferJumpActivated = -1;

    #region Direction
    private bool _isFacingRight = true;
    private int _facingDirection = 1;
    #endregion
    
    private float _xInput;
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }
    public void SetDirection(float dir)
    {
        _xInput = dir;
    }
    
    private void FixedUpdate()
    {
        HandleMovement();
        HandleCollisions();
        HandleFlip();
        HandleAnimation();
    }

    private void HandleMovement()
    {
        _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);
    }

    public void HandleJump(bool isPressed)
    {
        if (isPressed)
        {
            if (_isGrounded)
            {
                _rb.AddForce(new Vector2(_rb.velocity.x, jumpForce), ForceMode2D.Impulse);
            }
        }
        
        else if (_rb.velocity.y > 0) // уменьшаем прыжок, если кнопка не нажата.
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    public void HandleAnimation()
    {
        Vector3 velocityNormalized = _rb.velocity.normalized;
        _animator.SetFloat(XVelocityKey, velocityNormalized.x);
        _animator.SetFloat(YVelocityKey, velocityNormalized.y);
        _animator.SetBool(IsGroundedKey, _isGrounded);
    }

    #region Flip
    private void HandleFlip()
    {
        if (_rb.velocity.x < 0 && _isFacingRight || _rb.velocity.x > 0 && !_isFacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        _facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion
    
    private void HandleCollisions()
    {
        _isGrounded = Physics2D.CircleCast(
            transform.position + groundCheckDistance, 
            groundCheckRadius, 
            Vector2.down ,
            0, 
            whatIsGround );
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + groundCheckDistance, groundCheckRadius);
    }
}
