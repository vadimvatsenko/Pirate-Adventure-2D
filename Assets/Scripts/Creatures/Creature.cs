using System;
using UnityEngine;

namespace Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Attack Power Info")] 
        [SerializeField] protected int attackPower = 1;
        
        [Header("Movement Info")] 
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;
        
        [Header("Knockback Info")] 
        [SerializeField] protected float knockbackDuration;
        [SerializeField] protected Vector2 knockbackPower; 
        protected bool isKnocked;

        [Header("Die Info")] 
        [SerializeField] protected float maxSaveHieght = 20f;
        protected bool isDead;
        protected bool isAllreadyDead;
        
        protected CreatureCollisionInfo CollisionInfo;
        protected CratureAnimController CratureAnimationController;
        
        protected bool isFacingRight = true;
        protected bool isAirborne;
        
        protected Rigidbody2D rb;
        protected Collider2D c2d;
        protected Animator animator;
        public Rigidbody2D Rb => rb;
        public bool IsDead => isDead;
        public bool IsAirborne => isAirborne;
        public int FacingDirection { get; protected set; } = 1;
        protected float _xInput;

        private event Action OnCreatureJump;
        private event Action OnCreatureAttack;
        private event Action OnCreatureDeath;
        
        protected virtual void Awake()
        {
            CollisionInfo = GetComponent<CreatureCollisionInfo>();
            CratureAnimationController = GetComponent<CratureAnimController>();
            rb = GetComponent<Rigidbody2D>();
            c2d = GetComponent<Collider2D>();
            
            if (CratureAnimationController != null)
            {
                animator = CratureAnimationController.CreatureAnimator;
            }
        }
        
        protected virtual void FixedUpdate()
        {
            UpdateAirBornStatus();
            CollisionInfo.HandleGroundCheck();
            CollisionInfo.HandleWallCheck();
            
            if (isDead && CollisionInfo.IsGrounded) Die();
            
            //if (isKnocked || _isTeleporting || isDead || isAllreadyDead) return; 
            if (isKnocked ||  isDead || isAllreadyDead) return; 
            
            CheckDeathFalling();
            CratureAnimationController.HandleAnimation();
            HandleMovement();
            HandleFlip();
            
        }
        
        private void UpdateAirBornStatus()
        {
            if (CollisionInfo.IsGrounded && isAirborne) HandleLanding();
            if (!CollisionInfo.IsGrounded && !isAirborne) BecomeAirborn();
        }

        private void BecomeAirborn()
        {
            isAirborne = true;
        }

        protected virtual void HandleLanding()
        {
            isAirborne = false;
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
        
        public float XInput => _xInput;
        
        public void SetDirection(float dir) => _xInput = dir;
        protected virtual void HandleMovement()
        {
            rb.velocity = new Vector2(_xInput * speed, rb.velocity.y);
        }
        protected void HandleFlip()
        {
            if (rb.velocity.x < 0 && isFacingRight || rb.velocity.x > 0 && !isFacingRight)
            {
                Flip();
            }
        }

        protected void Flip()
        {
            isFacingRight = !isFacingRight;
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
        
        private void CheckDeathFalling()
        {
            if (!CollisionInfo.IsGrounded && !isDead)
            {
                isDead = Mathf.Abs(rb.velocity.y) > maxSaveHieght;
            }
        }
        
        protected virtual void Die()
        {
            if (!isAllreadyDead)
            {
                CratureAnimationController.SetDieAnimation();
                rb.velocity = new Vector2(knockbackPower.x / 2 * -FacingDirection, 0);
                rb.isKinematic = true;
                
                isAllreadyDead = true;
                // событие
                CallEventOnCreatureDeath();
            }
        }
    }
}