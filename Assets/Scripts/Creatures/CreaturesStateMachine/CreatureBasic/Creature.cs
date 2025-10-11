using System;
using Creatures.AnimationControllers;
using Creatures.CreaturesCollisions;
using Creatures.Interfaces;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class Creature : BasicCreature, IHitable
    {
        // Settings
        [Header("Movement info")] 
        [SerializeField] private float movementSpeed = 1.5f;
        
        [Header("Jump Info")]
        [SerializeField] protected float jumpForce;
        
        // Properties
        public float MovementSpeed => movementSpeed;
        public float JumpForce => jumpForce;
        
        // Components
        public CreatureCollisionInfo CollisionInfo { get; private set; }
        public CreatureHandleStateChange HandleStateChange { get; protected set; }
        
        public float XInput { get; protected set; }
        
        protected virtual void Awake()
        {
            base.Awake();
            CollisionInfo = GetComponent<CreatureCollisionInfo>();
            HandleStateChange = new CreatureHandleStateChange(this, StateMachine);
        }
        
        protected virtual void Update()
        {
            CollisionInfo.HandleGroundCheck();
            CollisionInfo.HandleWallCheck();
            
            UpdateAnimationVelocity();
            StateMachine.CurrentState.Update();
        }
        
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
    }
}