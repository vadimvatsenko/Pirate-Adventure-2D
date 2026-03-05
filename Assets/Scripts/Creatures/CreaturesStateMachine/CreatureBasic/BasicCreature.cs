using System;
using Creatures.Interfaces;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public abstract class BasicCreature : MonoBehaviour, IFacingDirection
    {
        [Header("Start Facing Direction")]
        [SerializeField] private int startFacingDirection;
        [Header("Hit Info")] 
        [SerializeField] private Vector2 hitPower = new Vector2(1.5f, 3f);
        [SerializeField] private float hitDuration = 0.5f;
        [Space] // пример для более тяжелого получения урона
        [SerializeField] private Vector2 heavyHitPower = new Vector2(7f, 7f);
        [SerializeField] private float heavyHitDuration = 1f;
        [SerializeField] private float heavyDamageThreshold = 0.3f;
        
        public Rigidbody2D Rb2D { get; private set; }
        public Collider2D C2D { get; private set; }
        public Animator AnimController { get; private set; }
        public BasicStateMachine StateMachine { get; private set; }

        // direction
        public int FacingDirection { get; private set; }
        public bool IsFacingRight { get; private set; } = true;
        //
        
        private Vector2 _finalHit; // финальное направление при получении удара
        private float _finalHitDuration;
        
        // properties
        public Vector2 HitPower => hitPower;
        public float HitDuration => hitDuration;
        public Vector2 HeavyHitPower => heavyHitPower;
        public float HeavyHitDuration => heavyHitDuration;
        public float HeavyDamageThreshold => heavyDamageThreshold;
        public Vector2 FinalHit => _finalHit;
        public float FinalHitDuration => _finalHitDuration;
        
        
        public event Action OnAttackEvent;
        public event Action OnDeathEvent;
        public event Action<int> OnThrowEvent; 
        
        // States
        public BasicState IdleState { get; protected set; }
        public BasicState MoveState { get; protected set; }
        public BasicState JumpState { get; protected set; }
        public BasicState DoubleJumpState { get; protected set; }
        public BasicState AttackState { get; protected set; }
        public BasicState FallState { get; protected set; }
        public BasicState HitState { get; protected set; }
        public BasicState DeathState { get; protected set; }
        public BasicState ClimbState { get; protected set; }
        public BasicState ThrowState { get; protected set; }
        public BasicState PauseState { get; protected set; }
        
        
        // Attack Event
        public void CallOnAttackEvent() => OnAttackEvent?.Invoke();
        public void SubscribeOnAttackEvent(Action action) => OnAttackEvent += action;
        public void UnsubscribeOnAttackEvent(Action action) => OnAttackEvent -= action;
        // Death Event
        public void CallOnDeathEvent() => OnDeathEvent?.Invoke();
        public void SubscribeOnDeathEvent(Action action) => OnDeathEvent += action;
        public void UnsubscribeOnDeathEvent(Action action) => OnDeathEvent -= action;
        // Throw Event - бросок
        public void CallOnThrowEvent(int amount = 1) => OnThrowEvent?.Invoke(amount);
        public void SubscribeOnThrowEvent(Action<int> action) => OnThrowEvent += action;
        public void UnsubscribeOnThrowEvent(Action<int> action) => OnThrowEvent -= action;

        protected virtual void Awake()
        {
            FacingDirection = startFacingDirection;
            Rb2D = GetComponent<Rigidbody2D>();
            C2D = GetComponent<Collider2D>();
            AnimController = GetComponentInChildren<Animator>();
            StateMachine = new BasicStateMachine();
        }

        protected virtual void Update()
        {
            StateMachine.CurrentState.Update();
        }
        
        public void SetFinalHit(Vector2 finalHit) => _finalHit = finalHit;
        public void SetFinalHitDuration(float duration) => _finalHitDuration = duration;
        
        public void Flip()
        {
            IsFacingRight = !IsFacingRight;
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }

        public void HandleIdleState() => StateMachine.ChangeState(IdleState);
        public void HandleMoveState() => StateMachine.ChangeState(MoveState);
        public void HandleJumpState() => StateMachine.ChangeState(JumpState);
        public void HandleDoubleJumpState() => StateMachine.ChangeState(DoubleJumpState);
        public void HandleAttackState() => StateMachine.ChangeState(AttackState);
        public void HandleFallState() => StateMachine.ChangeState(FallState);
        public void HandleHitState() => StateMachine.ChangeState(HitState);
        public void HandleDeathState() => StateMachine.ChangeState(DeathState);
        public void HandleClimbState() => StateMachine.ChangeState(ClimbState);
        public void HandleThrowState() => StateMachine.ChangeState(ThrowState);
        public void HandlePauseState() => StateMachine.ChangeState(PauseState);
        
    }
}