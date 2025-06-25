using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class Sharky : Creature
    {
        [Header("Idle Info")] 
        [SerializeField] private float idleDuration = 2f;
        private SharkyStatesController _sharkyStatesController;
        
        protected override void Awake()
        {
            base.Awake();

            //_sharkyStatesController = new SharkyStatesController(this, StateMachine, CollisionInfo);
            
            IdleState = new SharkyIdleState(this, StateMachine, AnimatorHashes.Idle, idleDuration);
            MoveState = new SharkyMoveState(this, StateMachine, AnimatorHashes.Move);
            JumpState = new SharkyJumpState(this, StateMachine, AnimatorHashes.Jump, jumpForce);
            
            StateMachine.Initialize(IdleState);
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            
            CollisionInfo.HandleAbyssCheck();
            CollisionInfo.HandleGroundAfterAbyssCheck();
            
            
            //_sharkyStatesController.Update();
        }

        public override void HandleFlip()
        {
            base.HandleFlip();
        }
    }
}