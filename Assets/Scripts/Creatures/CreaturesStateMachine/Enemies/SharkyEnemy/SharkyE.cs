using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyE : Creature
    {
        [Header("Idle Info")] 
        [SerializeField] private float idleDuration = 2f;
        public float IdleDuration => idleDuration;
        
        [Header("Aggro Info")]
        [SerializeField] private float aggroDuration = 2f;
        public float AggroDuration => aggroDuration;

        [Header("Battle Info")] 
        [SerializeField] private float battleSpeed = 2.5f;
        [SerializeField] private float attackDistance = 2f;
        [SerializeField] private float battleTimeDuration = 5f;
        public float BattleSpeed => battleSpeed;
        public float BattleTimeDuration => battleTimeDuration;
        
        public SharkyCollisionInfo SharkyCollisionInfo { get; private set; }
        public SharkyIdleState IdleState { get; private set; }
        public SharkyMoveState MoveState { get; private set; }
        public SharkyJumpState JumpState { get; private set; }
        public SharkyFallState FallState { get; private set; }
        public SharkyAggroState AggroState { get; private set; }
        public SharkyAttackState AttackState {get; private set;}
        public SharkyBattleState BattleState { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            SharkyCollisionInfo = GetComponent<SharkyCollisionInfo>();
            
            IdleState = new SharkyIdleState(this, StateMachine, AnimatorHashes.Idle);
            MoveState = new SharkyMoveState(this, StateMachine, AnimatorHashes.Move);
            JumpState = new SharkyJumpState(this, StateMachine, AnimatorHashes.Jump);
            FallState = new SharkyFallState(this, StateMachine, AnimatorHashes.Fall);
            //AggroState = new SharkyAggroState(this, StateMachine, AnimatorHashes.Aggro);
            AttackState = new SharkyAttackState(this, StateMachine, AnimatorHashes.Attack);
            BattleState = new SharkyBattleState(this, StateMachine, AnimatorHashes.Battle);
            
            StateMachine.Initialize(IdleState);
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
            float battleAnimSpeed = BattleSpeed / MovementSpeed; 
            AnimController.SetFloat(AnimatorHashes.BattleAnimSpeed, battleAnimSpeed);
        }
    }
}