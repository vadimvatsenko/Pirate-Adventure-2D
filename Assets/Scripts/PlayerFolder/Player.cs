﻿using System;
using System.Collections;
using Components;
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
        #region Static Fields
        // что тут происходит, перевод string в hash
        private static readonly int XVelocityKey = Animator.StringToHash("xVelocity");
        private static readonly int YVelocityKey = Animator.StringToHash("yVelocity");
        private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
        private static readonly int Knockback = Animator.StringToHash("knockback");
        private static readonly int AttackKey = Animator.StringToHash("attack");
        #endregion
        
        private PlayerCollisionInfo _collisionInfo;
        
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
        
        // ++
        [Header("Animator Controllers")]
        [SerializeField] private AnimatorController withoutArmor;
        [SerializeField] private AnimatorController withArmor;
        private bool _isArmed = false;

        #region Direction
        private bool _isFacingRight = true;
        private int _facingDirection = 1;
        private float _xInput;
        private bool _isPressedJumpButton;
        #endregion

        private Rigidbody2D _rb;
        private Animator _animator;
        public event Action OnPlayerJump;
        public UnityEvent onPlayerTakeDamage;
        
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
        
        private void Awake()
        {
            _collisionInfo = GetComponent<PlayerCollisionInfo>();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
        }
        
        private void FixedUpdate()
        {
            UpdateAirBornStatus();

            if (_isKnocked || _isTeleporting) return; 

            HandleMovement();
            _collisionInfo.HandleGroundCheck();
            _collisionInfo.HandleWallCheck();
            HandleFlip();
            HandleAnimation();
        }

        public void SetDirection(float dir) => _xInput = dir;

        // ++
        public void ChangeArmedState()
        {
            _isArmed = !_isArmed;
            _animator.runtimeAnimatorController = _isArmed ? withArmor : withoutArmor;
        }
        
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

        public void HandleAnimation()
        {
            Vector3 velocityNormalized = _rb.velocity.normalized;
            _animator.SetFloat(XVelocityKey, velocityNormalized.x);
            _animator.SetFloat(YVelocityKey, velocityNormalized.y);
            _animator.SetBool(IsGroundedKey, _collisionInfo.IsGrounded);
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

        
        
        public void TakeDamage() 
        {
            if (_isKnocked) return; 

            onPlayerTakeDamage?.Invoke();
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
        public void Teleport(Vector3 targetPosition, Action OnDestination) => StartCoroutine(SmoothLift(targetPosition, OnDestination));
        
        private IEnumerator SmoothLift(Vector3 targetPosition, Action OnDestination)
        {
            _isTeleporting = true;
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
            
            OnDestination?.Invoke();
            
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            transform.rotation = Quaternion.Euler(0f, startRotationInTeleportY, 0f);
            transform.localScale = new Vector3(startScaleInTeleport, startScaleInTeleport, startScaleInTeleport);
            _isTeleporting = false;
        }
        
        #endregion
        
        public void Attack()
        {
            if (!_isArmed) return;
            _animator.SetTrigger(AttackKey);
            var gos = _collisionInfo.GetObjectsInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null)
                {
                    //var tempDamage = Random.Range(0f, 2f);
                    hp.ApplyDamage(1);
                }
            }
        }
    }
}
