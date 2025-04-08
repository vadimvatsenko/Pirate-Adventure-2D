using System.Collections;
using UnityEngine;

namespace PlayerFolder
{
    public class Player : MonoBehaviour
    {
        #region Static Fields
        // что тут происходит, перевод string в hash
        private static readonly int XVelocityKey = Animator.StringToHash("xVelocity");
        private static readonly int YVelocityKey = Animator.StringToHash("yVelocity");
        private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
        private static readonly int Knockback = Animator.StringToHash("knockback"); // ++
        #endregion

        [Header("Movement Info")] [SerializeField] private float speed;
        [SerializeField] private float jumpForce;

        [Header("Collision Info")] 
        private bool _isGrounded;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector3 groundCheckDistance;
        [SerializeField] private float groundCheckRadius;

        [Header("DoubleJump Info")]
        [SerializeField] private float doubleJumpForce;
        private bool _canDoubleJump;
        private bool _isAirborne;

        [Header("Knockback Info")] // ++
        [SerializeField] private float knockbackDuration; // ++
        [SerializeField] private Vector2 knockbackPower; // ++
        private bool _isKnocked; // ++

        #region Direction
        private bool _isFacingRight = true;
        private int _facingDirection = 1;
        private float _xInput;
        #endregion
        
        private Rigidbody2D _rb;
        private Animator _animator;

        public Rigidbody2D Rb => _rb;
        public float XInput => _xInput;

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
            UpdateAirBornStatus();
            
            if(_isKnocked) return; // ++ 
            
            HandleMovement();
            //HandleCollisions();
            HandleFlip();
            HandleAnimation();
        }
        
        private void UpdateAirBornStatus() 
        {  
            if (_isGrounded && _isAirborne) HandleLanding();   
            if (!_isGrounded && !_isAirborne) BecomeAirborn();  
        } 
        
        private void BecomeAirborn() 
        {  
            _isAirborne = true;  
        }  
        private void HandleLanding()
        {  
            _isAirborne = false;  
            _canDoubleJump = true;  
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

                if (_isAirborne && _canDoubleJump)
                {
                    HandleDoubleJump();
                }
            }

            else if (_rb.velocity.y > 0) // уменьшаем прыжок, если кнопка не нажата.
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
            }
        }
        
        private void HandleDoubleJump()
        {
            _canDoubleJump = false;
            _rb.velocity = new Vector2(_rb.velocity.x, doubleJumpForce);
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

        /*private void HandleCollisions()
        {
            _isGrounded = Physics2D.CircleCast(
                transform.position + groundCheckDistance,
                groundCheckRadius,
                Vector2.down,
                0,
                whatIsGround);
        }*/

        public void TakeDamage() // ++
        {
            if(_isKnocked) return; // ++

            _animator.SetTrigger(Knockback); // ++
            
            _rb.velocity = new Vector2(knockbackPower.x * -_facingDirection, knockbackPower.y); // ++
            StartCoroutine(KnockbackRoutione()); // ++
        }

        private IEnumerator KnockbackRoutione() // ++
        {
            _isKnocked = true; // ++
            yield return new WaitForSeconds(knockbackDuration); // ++
            _isKnocked = false; // ++
            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _isGrounded = Vector2.Dot(collision.contacts[0].normal, Vector2.up) > 0.5f;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) _isGrounded = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position + groundCheckDistance, groundCheckRadius);
        }
    }
}
