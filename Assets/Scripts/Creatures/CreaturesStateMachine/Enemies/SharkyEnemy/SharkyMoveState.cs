using UnityEngine;
using UnityEngine.UIElements;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyMoveState : CreatureMoveState
    {
        public SharkyMoveState(Creature creature, CreatureStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
           base.Enter();
        }

        public override void Update()
        {
            base.Update();
            
            Creature.HandleMovement();

            if (Creature.CollisionInfo.IsGroundAfterAbyssDetected 
                && Creature.CollisionInfo.IsAbyssDetected)
            {
                StateMachine.ChangeState(Creature.JumpState);
            }
            
            else if (Creature.CollisionInfo.IsAbyssDetected)
            {
                Creature.HandleFlip();
                StateMachine.ChangeState(Creature.IdleState);
            } 
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}