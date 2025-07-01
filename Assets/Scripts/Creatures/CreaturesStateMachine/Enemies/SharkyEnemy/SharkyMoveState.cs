using UnityEngine;
using UnityEngine.UIElements;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyMoveState : CreatureState
    {
        private readonly Sharky _sharky;
        public SharkyMoveState(Sharky sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
            _sharky = sharky;
        }
        
        public override void Enter()
        {
           base.Enter();
           Creature.SetDirection(Creature.FacingDirection);
        }

        public override void Update()
        {
            base.Update();
            
            _sharky.HandleMovement();
            
            if (Creature.CollisionInfo.IsGroundAfterAbyssDetected 
                && Creature.CollisionInfo.IsAbyssDetected)
            {
                StateMachine.ChangeState(_sharky.SharkyJumpState);
            }
            
            else if (Creature.CollisionInfo.IsAbyssDetected || Creature.CollisionInfo.IsWallDetected)
            {
                StateMachine.ChangeState(_sharky.SharkyIdleState);
                Creature.HandleFlip();
            } 
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}