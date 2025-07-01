using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class Sharky : Creature
    {
        [Header("Idle Info")] 
        [SerializeField] private float idleDuration = 2f;
        public float IdleDuration => idleDuration;
        
        public SharkyIdleState SharkyIdleState { get; private set; }
        public SharkyMoveState SharkyMoveState { get; private set; }
        public SharkyJumpState SharkyJumpState { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            SharkyIdleState = new SharkyIdleState(this, StateMachine, AnimatorHashes.Idle);
            SharkyMoveState = new SharkyMoveState(this, StateMachine, AnimatorHashes.Move);
            SharkyJumpState = new SharkyJumpState(this, StateMachine, AnimatorHashes.Jump);
            
            StateMachine.Initialize(SharkyIdleState);
        }
        
        protected override void Update()
        {
            base.Update();
            
            CollisionInfo.HandleAbyssCheck();
            CollisionInfo.HandleGroundAfterAbyssCheck();
        }

        public override void HandleFlip()
        {
            Flip();
        }
    }
}