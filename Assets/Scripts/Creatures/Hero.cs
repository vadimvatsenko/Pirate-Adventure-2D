using System;
using System.Collections;
using Components.HealthComponentFolder;
using Model;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures
{
    public class Hero : Creature, IMovable
    {
        private GameSession _gameSession;
        
        [Header("DoubleJump Info")] 
        [SerializeField] private float doubleJumpForce;
        private bool _canDoubleJump;
        private bool _isAirborne;
        
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
        
        
        private bool _isPressedJumpButton;
        
        public UnityEvent onPlayerTakeDamage;
        public UnityEvent onPlayerDeath;
        public UnityEvent OnSpawnObject;
        
        public bool IsAirborne => _isAirborne;
        public bool CanDoubleJump => _canDoubleJump;
        public bool IsPressedJumpButton => _isPressedJumpButton;

        protected override void Awake()
        {
            base.Awake();
        }
        
        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }
        
        private void FixedUpdate()
        {
            UpdateAirBornStatus();
            CollisionInfo.HandleGroundCheck();
            CollisionInfo.HandleWallCheck();
            
            if (isDead && CollisionInfo.IsGrounded) Die();
            
            if (isKnocked || _isTeleporting || isDead || isAllreadyDead) return; 
            
            CheckDeathFalling();
            CratureAnimationController.HandleAnimation();
            HandleMovement();
            HandleFlip();
            
        }
        
        private void UpdateAirBornStatus()
        {
            if (CollisionInfo.IsGrounded && _isAirborne) HandleLanding();
            if (!CollisionInfo.IsGrounded && !_isAirborne) BecomeAirborn();
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

        protected override void HandleMovement()
        {
            base.HandleMovement();
        }
        
        public void HandleJump(bool isPressedSpace)
        {
            _isPressedJumpButton = isPressedSpace;
            if (_isPressedJumpButton)
            {
                
                if (CollisionInfo.IsGrounded)
                {
                    CallEventOnCreatureJump(); // событие
                    rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
                }

                if (_isAirborne && _canDoubleJump)
                {
                    CallEventOnCreatureJump(); // событие
                    HandleDoubleJump();
                }
            }

            else if (rb.velocity.y > 0) // уменьшаем прыжок, если кнопка не нажата.
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        private void HandleDoubleJump()
        {
            _canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
        }
        
        
 
        public void TakeDamage() 
        {
            if (isKnocked) return; 

            onPlayerTakeDamage?.Invoke();
            CratureAnimationController.SetKnockbackAnimation();

            rb.velocity = new Vector2(knockbackPower.x * -FacingDirection, knockbackPower.y); 
            StartCoroutine(KnockbackRoutione()); 
        }
        
        private IEnumerator KnockbackRoutione() 
        {
            isKnocked = true; 
            yield return new WaitForSeconds(knockbackDuration); 
            isKnocked = false; 
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
            if (!_gameSession.PlayerData.isArmed || !CollisionInfo.IsGrounded) return;

            CallEventOnCreatureAttack(); //событие
            CratureAnimationController.SetAttackAnimation();
            var gos = CollisionInfo.GetObjectsInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<OtherHealthComponent>();
                if (hp != null)
                {
                    hp.ApplyDamage(attackPower);
                }
            }
        }
        
        private void CheckDeathFalling()
        {
            if (!CollisionInfo.IsGrounded && !isDead)
            {
                isDead = Mathf.Abs(rb.velocity.y) > maxSaveHieght;
            }
        }
        
        public void Die()
        {
            if (!isAllreadyDead)
            {
                CratureAnimationController.SetDieAnimation();
                rb.velocity = new Vector2(knockbackPower.x / 2 * -FacingDirection, 0);
                rb.isKinematic = true;
                
                if (_gameSession.PlayerData.isArmed)
                {
                    _gameSession.PlayerData.isArmed = false;
                    OnSpawnObject?.Invoke();
                }
                onPlayerDeath?.Invoke();
            
                isAllreadyDead = true;
                CallEventOnCreatureDeath(); // событие
            }
        }
    }
}
