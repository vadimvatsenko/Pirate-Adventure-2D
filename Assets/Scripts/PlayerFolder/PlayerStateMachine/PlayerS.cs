using UnityEngine;

namespace PlayerFolder
{
    public class PlayerS : Player
    {

        [Header("Movement")] [SerializeField] private float speed;
        [SerializeField] private float jumpForce;

        [Header("Collision Info")] private bool _isGrounded;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector3 groundCheckDistance;
        [SerializeField] private float groundCheckRadius;

        [Header("Buffer Jump")] [SerializeField]
        private float jumpBufferWindow;

        private float _bufferJumpActivated = -1;

        #region Direction

        private bool _isFacingRight = true;
        private int _facingDirection = 1;
        private float _xInput;
        public float XInput => _xInput;

        #endregion

        private Rigidbody2D _rb;
        public Rigidbody2D Rb => _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public override void SetDirection(float dir)
        {
            _xInput = dir;
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleCollisions();
            HandleFlip();
        }

        private void HandleMovement()
        {
            _rb.velocity = new Vector2(_xInput * speed, _rb.velocity.y);
        }

        public override void HandleJump(bool isPressed)
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
                Vector2.down,
                0,
                whatIsGround);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position + groundCheckDistance, groundCheckRadius);
        }
    }
}
