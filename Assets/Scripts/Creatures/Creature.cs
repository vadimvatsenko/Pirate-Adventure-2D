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
        
        protected Rigidbody2D rb;
        protected Collider2D c2d;
        protected Animator animator;
        public Rigidbody2D Rb => rb;
        public bool IsDead => isDead;
        public int FacingDirection { get; protected set; } = 1;
        protected float _xInput;

        public event Action OnCreatureJump;
        public event Action OnCreatureAttack;
        public event Action OnCreatureDeath;
        
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
        
        // событие можно вызвать только из самого класса, но можно сделать
        // такую обвертку
        public void CallEventOnCreatureJump() => OnCreatureJump?.Invoke();
        public void CallEventOnCreatureAttack() => OnCreatureAttack?.Invoke();
        public void CallEventOnCreatureDeath() => OnCreatureDeath?.Invoke();
        
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

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}