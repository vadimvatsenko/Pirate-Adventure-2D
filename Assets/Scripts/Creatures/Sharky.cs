using System;
using UnityEngine;

namespace Creatures
{
    public class Sharky : Creature
    {

        protected override void Awake()
        {
            base.Awake();
        }
        private void FixedUpdate()
        {
            CollisionInfo.CheckAbyss();
            CollisionInfo.HandleWallCheck();
            CollisionInfo.HandleGroundCheck();
            Movement();
            CratureAnimationController.HandleAnimation();
            HandleTurnAround();
        }

        private void Movement()
        {
            if (CollisionInfo.IsAbyssDetected)
            {
                Rb.velocity = new Vector2(speed * FacingDirection, Rb.velocity.y);
            }
        }

        private void HandleTurnAround()
        {
            if (!CollisionInfo.IsAbyssDetected || CollisionInfo.IsWallDetected)
            {
                Flip();
            }
        }

        private void Falling()
        {
            
        }
    }
}