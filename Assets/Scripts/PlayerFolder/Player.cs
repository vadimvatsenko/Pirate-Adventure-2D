using System;
using System.Collections;
using Components;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Color = UnityEngine.Color;

namespace PlayerFolder
{
    public class Player : MonoBehaviour, IMovable
    {
        #region Static Fields
        // что тут происходит, перевод string в hash
        private static readonly int XVelocityKey = Animator.StringToHash("xVelocity");
        private static readonly int YVelocityKey = Animator.StringToHash("yVelocity");
        private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
        private static readonly int Knockback = Animator.StringToHash("knockback"); // ++
        #endregion

        [Header("Movement Info")] [SerializeField]
        private float speed;
        [SerializeField] private float jumpForce;

        [Header("Ground Collision Info")] 
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector3 groundCheckOffset;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
        private bool _isGrounded;
        
        [Header("Wall Collision Info")] 
        [SerializeField] private Vector3 wallCheckOffset;
        [SerializeField] private Vector2 wallBoxSize = new Vector2(0.1f, 0.5f);
        public bool IsWallDetected { get; private set; }

        [Header("Interaction Collision Info")] 
        [SerializeField] private LayerMask whatIsInteraction;
        [SerializeField] private float interactionRadius;
        private bool _isInteraction;
        private Collider2D[] _interactionCollides = new Collider2D[1];
        
        [Header("DoubleJump Info")] 
        [SerializeField] private float doubleJumpForce;
        private bool _canDoubleJump;
        private bool _isAirborne;

        [Header("Knockback Info")] 
        [SerializeField] private float knockbackDuration; 
        
        [Header("Teleport Info")]
        [SerializeField] private float durationInTeleport = 1f; // длительность подъема
        [SerializeField] float targetInTeleportHeight = 1f; // на сколько поднять
        [SerializeField] private float elapsedInTeleport = 0f;
        private float _startInTeleportY;
        private float _endInTeleportY;
        [Space] 
        [SerializeField] float rotationAmountInTeleport = 360f;
        private float _startRotationInTeleportZ;
        private float _endRotationInTeleportZ;
        [Space] 
        [SerializeField] private float startScaleInTeleport = 1f;
        [SerializeField] private float endScaleInTeleport = 0.1f;

        [SerializeField] private Vector2 knockbackPower; 
        private bool _isKnocked; 

        #region Direction
        private bool _isFacingRight = true;
        private int _facingDirection = 1;
        private float _xInput;
        private bool _isPressedJumpButton;
        #endregion

        private Rigidbody2D _rb;
        private Animator _animator;
        public event Action OnPlayerJump;
        public UnityEvent OnPlayerTakeDamage;
        
        public Rigidbody2D Rb => _rb;
        public float XInput => _xInput;

        public int FacingDirection
        {
            get => _facingDirection;
            private set => _facingDirection = value;
        }
        public bool IsGrounded => _isGrounded;
        public bool IsAirborne => _isAirborne;
        public bool CanDoubleJump => _canDoubleJump;
        public bool IsPressedJumpButton => _isPressedJumpButton;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
        }
        
        private void FixedUpdate()
        {
            UpdateAirBornStatus();

            if (_isKnocked) return; 

            HandleMovement();
            HandleGroundCheck();
            HandleWallCheck();
            HandleFlip();
            HandleAnimation();
        }

        public void SetDirection(float dir) => _xInput = dir;
        
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
        
        public void HandleJump(bool isPressedSpace)
        {
            _isPressedJumpButton = isPressedSpace;
            if (_isPressedJumpButton)
            {
                
                if (_isGrounded)
                {
                    OnPlayerJump?.Invoke();
                    _rb.AddForce(new Vector2(_rb.velocity.x, jumpForce), ForceMode2D.Impulse);
                }

                if (_isAirborne && _canDoubleJump)
                {
                    OnPlayerJump?.Invoke();
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

        #region Detection
        private void HandleWallCheck()
        {
            IsWallDetected = 
                Physics2D.BoxCast(
                    transform.position + (wallCheckOffset * _facingDirection), 
                    wallBoxSize, 
                    0, 
                    Vector2.right * _facingDirection, 
                    0, 
                    whatIsGround);
        }

        private void HandleGroundCheck()
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                transform.position + groundCheckOffset, 
                groundBoxSize, 
                0f, 
                Vector2.down, 
                groundCheckDistance,
                whatIsGround);

            if (hit.collider != null)
            {
                float dot = Vector2.Dot(hit.normal, Vector2.up);
                _isGrounded = dot >= 0.7f;
            }
            else
            {
                _isGrounded = false;
            }
        }
        
        public void Interact()
        {
            var size =
                Physics2D.OverlapCircleNonAlloc
                (transform.position,
                    interactionRadius,
                    _interactionCollides,
                    whatIsInteraction);
            
            for (int i = 0; i < size; i++)
            {
                
                var interactable = _interactionCollides[i].GetComponent<InteractableComponent>();
                if (interactable != null)
                {
                    
                    interactable.Interact();
                } 
                
            }

        }
        #endregion
        
        public void TakeDamage() 
        {
            if (_isKnocked) return; 

            OnPlayerTakeDamage?.Invoke();
            _animator.SetTrigger(Knockback); 

            _rb.velocity = new Vector2(knockbackPower.x * -_facingDirection, knockbackPower.y); 
            StartCoroutine(KnockbackRoutione()); 
        }
        
        private IEnumerator KnockbackRoutione() 
        {
            _isKnocked = true; 
            yield return new WaitForSeconds(knockbackDuration); 
            _isKnocked = false; 
        }

        #region Teleport
        public void Teleport(Vector3 targetPosition) => StartCoroutine(SmoothLift(targetPosition));
        
        private IEnumerator SmoothLift(Vector3 targetPosition)
        {
            elapsedInTeleport = 0f; // сброс таймера
            _startInTeleportY = transform.position.y;
            _endInTeleportY = _startInTeleportY + targetInTeleportHeight;
            
            float startRotationInTeleportY = transform.rotation.eulerAngles.y;
            _startRotationInTeleportZ = transform.rotation.eulerAngles.z;
            _endRotationInTeleportZ = _startRotationInTeleportZ + rotationAmountInTeleport;

            while (elapsedInTeleport < durationInTeleport)
            {
                float newY = Mathf.Lerp(_startInTeleportY, _endInTeleportY, elapsedInTeleport / durationInTeleport);
                float newRotationZ = 
                    Mathf.Lerp(_startRotationInTeleportZ, _endRotationInTeleportZ, elapsedInTeleport / durationInTeleport);
                float newScale = Mathf.Lerp(startScaleInTeleport, endScaleInTeleport, elapsedInTeleport / durationInTeleport);
                
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                transform.rotation = Quaternion.Euler(0f, 0f, newRotationZ);
                transform.localScale = new Vector3(newScale, newScale, newScale);
                
                elapsedInTeleport += Time.deltaTime;
                yield return null;
            }

            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0f, startRotationInTeleportY, _startRotationInTeleportZ);
            transform.localScale = new Vector3(startScaleInTeleport, startScaleInTeleport, startScaleInTeleport);
        }

        #endregion
        
        private void OnDrawGizmos()
        {
            // Ground check box
            Vector3 groundCheckPos = transform.position + groundCheckOffset;
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(groundCheckPos, groundBoxSize);

            // Wall check box
            Gizmos.color = IsWallDetected ? Color.green : Color.red;
            Gizmos.DrawWireCube(transform.position + (wallCheckOffset * _facingDirection), wallBoxSize);
            
            // Interaction check circle
            Gizmos.color = _isInteraction ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}
