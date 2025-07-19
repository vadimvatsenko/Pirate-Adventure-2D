using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class Sharky : Creature
    {
        [Header("Idle Info")] 
        [SerializeField] private float idleDuration = 2f;
        public float IdleDuration => idleDuration;
        
        public SharkyCollisionInfo SharkyCollisionInfo { get; private set; }
        
        public SharkyIdleState SharkyIdleState { get; private set; }
        public SharkyMoveState SharkyMoveState { get; private set; }
        public SharkyJumpState SharkyJumpState { get; private set; }
        
        public SharkyAggroState SharkyAggroState { get; private set; }
        public SharkyAttackState SharkyAttackState {get; private set;}
        
        protected override void Awake()
        {
            base.Awake();
            
            SharkyCollisionInfo = GetComponent<SharkyCollisionInfo>();
            
            SharkyIdleState = new SharkyIdleState(this, StateMachine, AnimatorHashes.Idle);
            SharkyMoveState = new SharkyMoveState(this, StateMachine, AnimatorHashes.Move);
            SharkyJumpState = new SharkyJumpState(this, StateMachine, AnimatorHashes.Jump);
            SharkyAggroState = new SharkyAggroState(this, StateMachine, AnimatorHashes.Aggro);
            SharkyAttackState = new SharkyAttackState(this, StateMachine, AnimatorHashes.Attack);
            
            StateMachine.Initialize(SharkyIdleState);
        }
        
        protected override void Update()
        {
            base.Update();
            
            SharkyCollisionInfo.HandleAbyssCheck();
            SharkyCollisionInfo.HandleGroundAfterAbyssCheck();
            SharkyCollisionInfo.CreatureCheck();
        }

        public override void HandleFlip()
        {
            Flip();
        }
    }
}