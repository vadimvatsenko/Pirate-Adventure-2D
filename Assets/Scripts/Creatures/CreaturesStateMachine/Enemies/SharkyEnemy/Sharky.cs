using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class Sharky : Creature
    {
        [Header("Idle Info")] 
        [SerializeField] private float idleDuration = 2f;
        public float IdleDuration => idleDuration;
        
        [Header("Aggro Info")]
        [SerializeField] private float aggroDuration = 2f;
        public float AggroDuration => aggroDuration;

        [Header("Battle Details")] 
        [SerializeField] private float battleSpeed = 2.5f;
        public float BattleSpeed => battleSpeed;

        [SerializeField] private float attackDistance;
        
        public SharkyCollisionInfo SharkyCollisionInfo { get; private set; }
        public SharkyIdleState SharkyIdleState { get; private set; }
        public SharkyMoveState SharkyMoveState { get; private set; }
        public SharkyJumpState SharkyJumpState { get; private set; }
        public SharkyFallState SharkyFallState { get; private set; }
        
        public SharkyAggroState SharkyAggroState { get; private set; }
        public SharkyAttackState SharkyAttackState {get; private set;}
        public SharkyBattleState SharkyBattleState { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            SharkyCollisionInfo = GetComponent<SharkyCollisionInfo>();
            
            SharkyIdleState = new SharkyIdleState(this, StateMachine, AnimatorHashes.Idle);
            SharkyMoveState = new SharkyMoveState(this, StateMachine, AnimatorHashes.Move);
            SharkyJumpState = new SharkyJumpState(this, StateMachine, AnimatorHashes.Jump);
            SharkyFallState = new SharkyFallState(this, StateMachine, AnimatorHashes.Fall);
            SharkyAggroState = new SharkyAggroState(this, StateMachine, AnimatorHashes.Aggro);
            SharkyAttackState = new SharkyAttackState(this, StateMachine, AnimatorHashes.Attack);
            SharkyBattleState = new SharkyBattleState(this, StateMachine, AnimatorHashes.Battle);
            
            StateMachine.Initialize(SharkyIdleState);
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
    }
}