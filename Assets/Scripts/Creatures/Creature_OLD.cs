using System;
using System.Collections;
using Creatures.CreaturesStateMachine;
using UnityEngine;

namespace Creatures
{
    public class Creature_OLD : MonoBehaviour
    {
        
        [Header("Knock Info")] 
        [SerializeField] protected float knockDuration;
        protected bool IsKnocked;
        
        // air status
        public bool IsAirborne {get; protected set; }
        
        // events
        
        
        protected virtual void FixedUpdate()
        {
            /*UpdateAirBornStatus();
            
            CollisionInfo.HandleGroundCheck();
            CollisionInfo.HandleWallCheck();
            
            if (IsDead && CollisionInfo.IsGrounded) Die();
            
            //if (isKnocked || _isTeleporting || isDead || isAllreadyDead) return; 
            if (IsKnocked ||  IsDead || IsAllreadyDead || IsDead) return; 
            
            CheckDeathFalling();
            CreatureAnimationController.HandleAnimation();
            HandleMovement();
            HandleFlip();*/
            
        }
        
        public void TakeDamage() 
        {
            /*if (IsKnocked) return; 

            CallEventOnCreatureTakeDanage();
            CreatureAnimationController.SetKnockbackAnimation();

            Rb.velocity = new Vector2(knockPower.x * -FacingDirection, knockPower.y); 
            StartCoroutine(KnockbackRoutione()); */
        }
        
        private IEnumerator KnockbackRoutione() 
        {
            Debug.Log("KnockbackRoutione");
            IsKnocked = true; 
            yield return new WaitForSeconds(knockDuration); 
            IsKnocked = false; 
        }
    }
}