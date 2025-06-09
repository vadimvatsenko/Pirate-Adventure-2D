using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Attack Power Info")] 
        [SerializeField] protected int attackPower = 1;
        
        [Header("Movement Info")] 
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;
        
        [Header("Knock Info")] 
        [SerializeField] protected float knockDuration;
        [SerializeField] protected Vector2 knockPower; 
        protected bool IsKnocked;

        [Header("Die Info")] 
        [SerializeField] protected float maxSaveHieght = 20f;
        public bool IsDead { get; protected set; }
        public bool IsAllreadyDead { get; protected set; }
        
        // components
        protected CreatureCollisionInfo CollisionInfo;
        protected CratureAnimController CratureAnimationController;
        public Rigidbody2D Rb { get; protected set; }
        protected Collider2D C2d;
        protected Animator Animator;
        
        // directions
        public bool IsFacingRight { get; protected set; } = true;
        public int FacingDirection { get; protected set; } = 1;
        public float XInput { get; protected set; }
        
        // air status
        public bool IsAirborne {get; protected set; }
        
        // events
        private event Action OnCreatureJump;
        private event Action OnCreatureAttack;
        private event Action OnCreatureDeath;
        
        protected virtual void Awake()
        {
            CollisionInfo = GetComponent<CreatureCollisionInfo>();
            CratureAnimationController = GetComponent<CratureAnimController>();
            Rb = GetComponent<Rigidbody2D>();
            C2d = GetComponent<Collider2D>();
            
            if (CratureAnimationController != null)
            {
                Animator = CratureAnimationController.CreatureAnimator;
            }
        }
        
        protected virtual void FixedUpdate()
        {
            UpdateAirBornStatus();
            CollisionInfo.HandleGroundCheck();
            CollisionInfo.HandleWallCheck();
            
            if (IsDead && CollisionInfo.IsGrounded) Die();
            
            //if (isKnocked || _isTeleporting || isDead || isAllreadyDead) return; 
            if (IsKnocked ||  IsDead || IsAllreadyDead) return; 
            
            CheckDeathFalling();
            CratureAnimationController.HandleAnimation();
            HandleMovement();
            HandleFlip();
            
        }
        
        private void UpdateAirBornStatus()
        {
            if (CollisionInfo.IsGrounded && IsAirborne) HandleLanding();
            if (!CollisionInfo.IsGrounded && !IsAirborne) BecomeAirborn();
        }

        private void BecomeAirborn()
        {
            IsAirborne = true;
        }

        protected virtual void HandleLanding()
        {
            IsAirborne = false;
        }
        
        // событие можно вызвать только из самого класса, но можно сделать
        // такую обвертку
        public void CallEventOnCreatureAttack() => OnCreatureAttack?.Invoke();
        public void CallEventOnCreatureDeath() => OnCreatureDeath?.Invoke();
        public void CallEventOnCreatureJump() => OnCreatureJump?.Invoke();
        

        public void SubscribeOnCreatureDeath(Action onDeath) 
            => OnCreatureDeath += onDeath;
        public void UnsubscribeOnCreatureDeath(Action onDeath) 
            => OnCreatureDeath -= onDeath;
        public void SubscribeOnCreatureJump(Action onJump)
            => OnCreatureJump += onJump;
        public void UnsubscribeOnCreatureJump(Action onJump)
            => OnCreatureJump -= onJump;
        public void SubscribeOnCreatureAttack(Action onAttack)
            => OnCreatureAttack += onAttack;
        public void UnSubscribeCreatureAttack(Action onAttack)
            => OnCreatureAttack -= onAttack;
        
        
        public void SetDirection(float dir) => XInput = dir;
        protected virtual void HandleMovement()
        {
            Rb.velocity = new Vector2(XInput * speed, Rb.velocity.y);
        }
        private void HandleFlip()
        {
            if (Rb.velocity.x < 0 && IsFacingRight || Rb.velocity.x > 0 && !IsFacingRight)
            {
                Flip();
            }
        }

        protected void Flip()
        {
            IsFacingRight = !IsFacingRight;
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
        
        private void CheckDeathFalling()
        {
            if (!CollisionInfo.IsGrounded && !IsDead)
            {
                IsDead = Mathf.Abs(Rb.velocity.y) > maxSaveHieght;
            }
        }
        
        protected virtual void Die()
        {
            if (!IsAllreadyDead)
            {
                CratureAnimationController.SetDieAnimation();
                Rb.velocity = new Vector2(knockPower.x / 2 * -FacingDirection, 0);
                Rb.isKinematic = true;
                
                IsAllreadyDead = true;
                // событие
                CallEventOnCreatureDeath();
            }
        }
    }
}