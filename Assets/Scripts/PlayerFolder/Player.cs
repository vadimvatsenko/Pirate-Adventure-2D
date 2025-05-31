using System;
using System.Collections;
using Components;
using DefaultNamespace.Model;
using Interfaces;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using Color = UnityEngine.Color;
using Random = System.Random;

namespace PlayerFolder
{
    public class Player : MonoBehaviour, IMovable
    {
        private PlayerCollisionInfo _collisionInfo;
        private PlayerAnimController _playerAnimController;
        private GameSession _gameSession;

        [Header("Attack Power Info")] 
        [SerializeField] private int attackPower = 1;
        
        [Header("Movement Info")] [SerializeField]
        private float speed;
        [SerializeField] private float jumpForce;
        
        [Header("DoubleJump Info")] 
        [SerializeField] private float doubleJumpForce;
        private bool _canDoubleJump;
        private bool _isAirborne;

        [Header("Knockback Info")] 
        [SerializeField] private float knockbackDuration;
        [SerializeField] private Vector2 knockbackPower; 
        private bool _isKnocked;

        [Header("Die Info")] 
        [SerializeField] private float maxSaveHieght = 20f;
        private bool _isDead;
        private bool _isAllreadyDead;
        
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
        private bool _isTeleporting;
        
        #region Direction
        private bool _isFacingRight = true;
        private int _facingDirection = 1;
        private float _xInput;
        private bool _isPressedJumpButton;
        #endregion

        private Rigidbody2D _rb;
        private Animator _animator;
        private Collider2D _collider;
        public event Action OnPlayerJump;
        public event Action OnPlayerAttack;
        public UnityEvent onPlayerTakeDamage;
        public UnityEvent onPlayerDeath;
        public UnityEvent OnSpawnObject;
        
        public Rigidbody2D Rb => _rb;
        public float XInput => _xInput;

        public int FacingDirection
        {
            get => _facingDirection;
            private set => _facingDirection = value;
        }
        
        public bool IsAirborne => _isAirborne;
        public bool CanDoubleJump => _canDoubleJump;
        public bool IsPressedJumpButton => _isPressedJumpButton;
        public bool IsDead => _isDead;

        private void Awake()
        {
            _collisionInfo = GetComponent<PlayerCollisionInfo>();
            _playerAnimController = GetComponent<PlayerAnimController>();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            
            if (_playerAnimController != null)
            {
                _animator = _playerAnimController.PlayerAnimator;
            }
        }
        
        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }
        
        private void FixedUpdate()
        {
            UpdateAirBornStatus();
            _collisionInfo.HandleGroundCheck();
            _collisionInfo.HandleWallCheck();
            
            if (_isDead && _collisionInfo.IsGrounded) Die();
            
            if (_isKnocked || _isTeleporting || _isDead) return; 
            
            CheckDeathFalling();
            _playerAnimController.HandleAnimation();
            HandleMovement();
            HandleFlip();
        }
        

        public void SetDirection(float dir) => _xInput = dir;
        
        private void UpdateAirBornStatus()
        {
            if (_collisionInfo.IsGrounded && _isAirborne) HandleLanding();
            if (!_collisionInfo.IsGrounded && !_isAirborne) BecomeAirborn();
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
                
                if (_collisionInfo.IsGrounded)
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
 
        public void TakeDamage() 
        {
            if (_isKnocked) return; 

            onPlayerTakeDamage?.Invoke();
            _playerAnimController.SetKnockbackAnimation();

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
        public void Teleport(Vector3 targetPosition, Action OnDestination) => StartCoroutine(SmoothLift(targetPosition, OnDestination));
        
        private IEnumerator SmoothLift(Vector3 targetPosition, Action OnDestination)
        {
            _isTeleporting = true;
            elapsedInTeleport = 0f; // сброс таймера
            
            _startInTeleportY = transform.position.y;
            _endInTeleportY = _startInTeleportY + targetInTeleportHeight;
            
            float startRotationInTeleportY = transform.eulerAngles.y;
            float endRotationInTeleportY = startRotationInTeleportY;
            
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
            
            OnDestination?.Invoke();
            
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0f, endRotationInTeleportY, 0f);
            transform.localScale = new Vector3(startScaleInTeleport, startScaleInTeleport, startScaleInTeleport);
            _isTeleporting = false;
        }
        
        #endregion
        
        public void Attack()
        {
            if (!_playerAnimController.IsArmed || !_collisionInfo.IsGrounded) return;
            
            OnPlayerAttack?.Invoke();
            _playerAnimController.SetAttackAnimation();
            var gos = _collisionInfo.GetObjectsInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null)
                {
                    hp.ApplyDamage(attackPower);
                }
            }
        }

        public void SitDown()
        {
            _playerAnimController.SetSitAnimation();
        }

        private void CheckDeathFalling()
        {
            if (!_collisionInfo.IsGrounded && !_isDead)
            {
                _isDead = Mathf.Abs(_rb.velocity.y) > maxSaveHieght;
            }
        }
        
        

        public void Die()
        {
            if (!_isAllreadyDead)
            {
                _playerAnimController.SetDieAnimation();
                _rb.velocity = new Vector2(knockbackPower.x / 2 * -_facingDirection, 0);
                _rb.isKinematic = true;
                
                if (_playerAnimController.IsArmed)
                {
                    OnSpawnObject?.Invoke();
                }
                onPlayerDeath?.Invoke();
            
                _isAllreadyDead = true;
            }
        }
    }
}
