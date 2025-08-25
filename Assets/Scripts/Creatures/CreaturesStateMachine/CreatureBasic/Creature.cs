using System;
using Creatures.AnimationControllers;
using Creatures.CreaturesCollisions;
using Creatures.Interfaces;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class Creature : MonoBehaviour, IFacingDirection
    {
        // Settings
        [Header("Movement info")] 
        [SerializeField] private float movementSpeed = 1.5f;
        
        [Header("Jump Info")]
        [SerializeField] protected float jumpForce;
        
        [Header("Die Info")] 
        [SerializeField] private float dieHeight = 5f;
        
        [Header("Hit Info")] 
        [SerializeField] private Vector2 hitPower;
        [SerializeField] private float hitDuration;
        private Vector2 _finalHit;
        
        // Properties
        public float MovementSpeed => movementSpeed;
        public float JumpForce => jumpForce;
        public Vector2 HitPower => hitPower;
        public float HitDuration => hitDuration;
        public Vector2 FinalHit => _finalHit;
        //

        // Components
        public Animator AnimController { get; protected set; }
        public CreatureCollisionInfo CollisionInfo { get; private set; }
        public CreatureStateMachine StateMachine { get; protected set; }
        public Rigidbody2D Rb2D { get; private set; }
        public Collider2D C2D { get; protected set; }
        
        // Directions
        public bool IsFacingRight { get; protected set; } = true;
        public int FacingDirection { get; protected set; } = 1;
        public float XInput { get; protected set; }
        
        // States
        public CreatureState IdleState { get; protected set; }
        public CreatureState MoveState { get; protected set; }
        public CreatureState JumpState { get; protected set; }
        public CreatureState DoubleJumpState { get; protected set; }
        
        public CreatureState AttackState { get; protected set; }
        public CreatureState FallState { get; protected set; }
        public CreatureState HitState { get; protected set; }
        public CreatureState DeathState { get; protected set; }
        public CreatureState ClimbState { get; protected set; }
        
        // Events
        public event Action OnJumpEvent;
        public event Action OnAttackEvent;
        public event Action OnDeathEvent;
        
        protected virtual void Awake()
        {
            Rb2D = GetComponent<Rigidbody2D>();
            C2D = GetComponent<Collider2D>();
            
            AnimController = GetComponentInChildren<Animator>();
            
            CollisionInfo = GetComponent<CreatureCollisionInfo>();
            StateMachine = new CreatureStateMachine();
        }
        
        protected virtual void Update()
        {
            CollisionInfo.HandleGroundCheck();
            CollisionInfo.HandleWallCheck();
            
            UpdateAnimationVelocity();
            StateMachine.CurrentState.Update();
        }
        
        // CallEvents
        // JumpEvent
        public void CallOnJumpEvent() => OnJumpEvent?.Invoke();
        public void SubscribeOnJumpEvent(Action action) => OnJumpEvent += action;
        public void UnsubscribeOnJumpEvent(Action action) => OnJumpEvent -= action;
        // Attack Event
        public void CallOnAttackEvent() => OnAttackEvent?.Invoke();
        public void SubscribeOnAttackEvent(Action action) => OnAttackEvent += action;
        public void UnsubscribeOnAttackEvent(Action action) => OnAttackEvent -= action;
        // Death Event
        public void CallOnDeathEvent() => OnDeathEvent?.Invoke();
        public void SubscribeOnDeathEvent(Action action) => OnDeathEvent += action;
        public void UnsubscribeOnDeathEvent(Action action) => OnDeathEvent -= action;
        
        public void SetDirection(float dir) => XInput = dir;
        
        public virtual void HandleMovement()
        {
            Rb2D.velocity = new Vector2(XInput * movementSpeed, Rb2D.velocity.y);
        }

        protected virtual void UpdateAnimationVelocity()
        {
            AnimController.SetFloat(AnimatorHashes.YVelocity, Rb2D.velocity.y);
            AnimController.SetFloat(AnimatorHashes.XVelocity, Rb2D.velocity.x);
        }
        
        public virtual void HandleFlip()
        {
            if (XInput < 0 && IsFacingRight || XInput > 0 && !IsFacingRight)
            {
                Flip();
            }
        }
        
        public void Flip()
        {
            IsFacingRight = !IsFacingRight;
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }

        public void SetFinalHit(Vector2 finalHit) => _finalHit = finalHit;
        
        protected int TakeHit(Creature damager) => damager.FacingDirection;
        public void DestroySelf() => Destroy(gameObject);
    }
}