using System;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyE : Creature
    {
        [Header("Idle Info")] 
        [SerializeField] private float idleDuration = 2f;
        public float IdleDuration => idleDuration;
        
        [Header("Aggro Info")]
        [SerializeField] private float aggroDuration = 0.75f;
        public float AggroDuration => aggroDuration;

        [Header("Battle Info")] 
        [SerializeField] private float battleSpeed = 2.5f;
        [SerializeField] private float battleTimeDuration = 5f;
        // отскок от игрока при атаке
        [SerializeField] private float minRetreatDistance = 1f;
        [SerializeField] private Vector2 retreatVelocity = Vector2.zero;
        // свойства боевого состояния
        public float BattleSpeed => battleSpeed;
        public float BattleTimeDuration => battleTimeDuration;
        public float MinRetreatDistance => minRetreatDistance;
        public Vector2 RetreatVelocity => retreatVelocity;
        //
        public Hero Hr { get; private set; }
        public SharkyCollisionInfo SharkyCollisionInfo { get; private set; }
        public SharkyAggroState AggroState { get; private set; }
        public SharkyAttackState AttackState {get; private set;}
        public SharkyBattleState BattleState { get; private set; }
        public SharkyRespawnState RespawnState { get; private set; }
        
        // Aggro Event
        public event Action OnAgroEvent;
        public void CallOnAgroEvent() => OnAgroEvent?.Invoke();
        public void SubscribeOnAgroEvent(Action action) => OnAgroEvent += action;
        public void UnsubscribeOnAgroEvent(Action action) => OnAgroEvent -= action;
        // 
        // WTF Event
        public event Action OnWTFEvent;
        public void CallOnWTFEvent() => OnWTFEvent?.Invoke();
        public void SubscribeOnWTFEvent(Action action) => OnWTFEvent += action;
        public void UnsubscribeOnWTFEvent(Action action) => OnWTFEvent -= action;
        
        
        protected override void Awake()
        {
            base.Awake();
            
            SharkyCollisionInfo = GetComponent<SharkyCollisionInfo>();
            
            IdleState = new SharkyIdleState(this, StateMachine, AnimatorHashes.Idle);
            MoveState = new SharkyMoveState(this, StateMachine, AnimatorHashes.Move);
            JumpState = new SharkyJumpState(this, StateMachine, AnimatorHashes.Jump);
            FallState = new SharkyFallState(this, StateMachine, AnimatorHashes.Fall);
            AggroState = new SharkyAggroState(this, StateMachine, AnimatorHashes.Aggro);
            AttackState = new SharkyAttackState(this, StateMachine, AnimatorHashes.Attack);
            BattleState = new SharkyBattleState(this, StateMachine, AnimatorHashes.Battle);
            HitState = new SharkyHitState(this, StateMachine, AnimatorHashes.Hit);
            DeathState = new SharkyDeathState(this, StateMachine, AnimatorHashes.Death);
            //RespawnState = new SharkyRespawnState(this, StateMachine, AnimatorHashes.Respawn);
            StateMachine.Initialize(IdleState);
        }

        private void Start()
        {
            Hr = FindObjectOfType<Hero>();
        }
        
        protected override void Update()
        {
            base.Update();
            
            SharkyCollisionInfo.HandleAbyssCheck();
            SharkyCollisionInfo.HandleGroundAfterAbyssCheck();
        }

        public override void HandleFlip()
        {
            Flip();
        }

        public override void HandleMovement()
        {
            Rb2D.velocity = new Vector2(MovementSpeed * FacingDirection, Rb2D.velocity.y);
        }
        
        protected override void UpdateAnimationVelocity()
        {
            base.UpdateAnimationVelocity();
            
            // это множитель для анимации. Он будет увеличивать скорость анимации преследования игрока
            // в редакторе в Аниматоре, нажимаем на blandTree в инспекторе Multyply => parameters => battleAnimSpeed
            float battleAnimSpeed = BattleSpeed / MovementSpeed; 
            AnimController.SetFloat(AnimatorHashes.BattleAnimSpeed, battleAnimSpeed);
        }
    }
}