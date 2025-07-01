using System;
using System.Collections;
using Creatures.CreaturesStateMachine;
using UnityEngine;

namespace Creatures
{
    public class Creature_OLD : MonoBehaviour
    {
        [Header("Attack Power Info")] 
        [SerializeField] protected int attackPower = 1;
        
        /*[Header("Movement Info")] 
        [SerializeField] protected float speed;
        [SerializeField] protected float jumpForce;*/
        
        [Header("Knock Info")] 
        [SerializeField] protected float knockDuration;
        [SerializeField] protected Vector2 knockPower; 
        protected bool IsKnocked;

        [Header("Die Info")] 
        [SerializeField] protected float maxSaveHieght = 20f;
        public bool IsDead { get; protected set; }
        public bool IsAllreadyDead { get; protected set; }
        
        
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
        
        
        
        
        private void CheckDeathFalling()
        {
            /*if (!CollisionInfo.IsGrounded && !IsDead)
            {
                IsDead = Mathf.Abs(Rb.velocity.y) > maxSaveHieght;
            }*/
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

        public virtual void Attack()
        {
            //CreatureAnimationController.SetAttackAnimation();
        }

        public virtual void Die() => StartCoroutine(DieRoutine());
        
        private IEnumerator DieRoutine()
        {
            
            if (!IsAllreadyDead)
            {
                /*CreatureAnimationController.SetDieAnimation();
                Rb.velocity = new Vector2(knockPower.x / 2 * -FacingDirection, 0);
                
                IsAllreadyDead = true;
                
                yield return new WaitForSeconds(2f);
                Rb.isKinematic = true;
                
                Debug.Log(this.name + " is dead");
                CallEventOnCreatureDeath();*/
            }

            yield return null;
        }
    }
}