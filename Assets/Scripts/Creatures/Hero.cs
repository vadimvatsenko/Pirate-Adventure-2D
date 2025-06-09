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
        private bool _isPressedJumpButton;
        
        public UnityEvent onPlayerTakeDamage;
        public UnityEvent onPlayerDeath;
        public UnityEvent OnSpawnObject;
        
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

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        
        protected override void HandleMovement()
        {
            base.HandleMovement();
        }

        protected override void HandleLanding()
        {
            base.HandleLanding();
            _canDoubleJump = true;
        }
        
        public void HandleJump(bool isPressedSpace)
        {
            _isPressedJumpButton = isPressedSpace;
            if (_isPressedJumpButton)
            {
                
                if (CollisionInfo.IsGrounded)
                {
                    CallEventOnCreatureJump(); // событие
                    Rb.AddForce(new Vector2(Rb.velocity.x, jumpForce), ForceMode2D.Impulse);
                }

                if (IsAirborne && _canDoubleJump)
                {
                    CallEventOnCreatureJump(); // событие
                    HandleDoubleJump();
                }
            }

            else if (Rb.velocity.y > 0) // уменьшаем прыжок, если кнопка не нажата.
            {
                Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * 0.5f);
            }
        }

        private void HandleDoubleJump()
        {
            _canDoubleJump = false;
            Rb.velocity = new Vector2(Rb.velocity.x, doubleJumpForce);
        }
        
        public void TakeDamage() 
        {
            if (IsKnocked) return; 

            onPlayerTakeDamage?.Invoke();
            CratureAnimationController.SetKnockbackAnimation();

            Rb.velocity = new Vector2(knockPower.x * -FacingDirection, knockPower.y); 
            StartCoroutine(KnockbackRoutione()); 
        }
        
        private IEnumerator KnockbackRoutione() 
        {
            IsKnocked = true; 
            yield return new WaitForSeconds(knockDuration); 
            IsKnocked = false; 
        }
        
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
        
        protected override void Die()
        {
            base.Die();
            if (_gameSession.PlayerData.isArmed)
            {
                _gameSession.PlayerData.isArmed = false;
                OnSpawnObject?.Invoke();
            }
            onPlayerDeath?.Invoke();
        }
    }
}
