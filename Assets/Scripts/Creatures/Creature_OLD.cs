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
        
        // components
        
        //public CreatureCollisionInfo CollisionInfo { get; private set; }
        //protected CreatureAnimController CreatureAnimationController;
        //public Rigidbody2D Rb { get; private set; }
        //protected Collider2D C2d;
        //protected Animator Animator;
        
        // directions
        //public bool IsFacingRight { get; protected set; } = true;
        //public int FacingDirection { get; private set; } = 1;
        //public float XInput { get; protected set; }
        
        // air status
        public bool IsAirborne {get; protected set; }
        
        // events
        private event Action OnCreatureJump;
        private event Action OnCreatureAttack;
        private event Action OnCreatureDeath;
        private event Action OnCreatureTakeDamage;
        
        protected virtual void Awake()
        {
            /*CollisionInfo = GetComponent<CreatureCollisionInfo>();
            CreatureAnimationController = GetComponent<CreatureAnimController>();
            Rb = GetComponent<Rigidbody2D>();
            C2d = GetComponent<Collider2D>();*/
            
            /*if (CreatureAnimationController != null)
            {
                Animator = CreatureAnimationController.CreatureAnimator;
            }*/
        }
        
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
        
        private void UpdateAirBornStatus()
        {
            /*if (CollisionInfo.IsGrounded && IsAirborne) HandleLanding();
            if (!CollisionInfo.IsGrounded && !IsAirborne) BecomeAirborn();*/
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
        public void CallEventOnCreatureTakeDanage() => OnCreatureTakeDamage?.Invoke();
        public void CallEventOnCreatureAttack() => OnCreatureAttack?.Invoke();
        public void CallEventOnCreatureDeath() => OnCreatureDeath?.Invoke();
        public void CallEventOnCreatureJump() => OnCreatureJump?.Invoke();
        
        public void SubscribeOnCreatureTakeDamage(Action onCreatureTakeDamage)
            => OnCreatureTakeDamage += onCreatureTakeDamage;
        public void UnsubscribeOnCreatureTakeDamage(Action onCreatureTakeDamage)
            => OnCreatureTakeDamage -= onCreatureTakeDamage;
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
        
        
        // public void SetDirection(float dir) => XInput = dir;
        /*protected virtual void HandleMovement()
        {
            Rb.velocity = new Vector2(XInput * speed, Rb.velocity.y);
        }*/
        /*private void HandleFlip()
        {
            if (Rb.velocity.x < 0 && IsFacingRight || Rb.velocity.x > 0 && !IsFacingRight)
            {
                Flip();
            }
        }*/

        //public void CallFlip() => Flip();
        /*protected void Flip()
        {
            IsFacingRight = !IsFacingRight;
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }*/
        
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