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
            CollisionInfo.CheckGroundBeforeCreature();
            CollisionInfo.HandleWallCheck();
            CollisionInfo.HandleGroundCheck();
            Movement();
            CratureAnimationController.HandleAnimation();
            HandleTurnAround();
        }

        private void Movement()
        {
            if (!CollisionInfo.IsAbyssDetected)
            {
                // Движемся вперёд
                Rb.velocity = new Vector2(speed * FacingDirection, Rb.velocity.y);
                IsAirborne = false;
            }
            else if (CollisionInfo.IsAbyssDetected && CollisionInfo.IsGroundAfterAbyss && !IsAirborne)
            {
                // Прыгаем только один раз
                Vector2 bounceDirection = new Vector2(FacingDirection * 3f, 2.5f);
                Rb.AddForce(bounceDirection, ForceMode2D.Impulse);
                IsAirborne = true;
            }
        }

        private void HandleTurnAround()
        {
            if (CollisionInfo.IsWallDetected || (!CollisionInfo.IsGroundAfterAbyss && CollisionInfo.IsAbyssDetected) && !IsAirborne)
            {
                Flip();
            }
        }

        protected override void Die()
        {
            base.Die();
        }
    }
}