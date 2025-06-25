using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyJumpState : CreatureJumpState
    {
        private float _jumpForce;
        public SharkyJumpState(Creature creature, CreatureStateMachine stateMachine, int animBoolName, float jumpForce) 
            : base(creature, stateMachine, animBoolName)
        {
            _jumpForce = jumpForce;
        }

        public override void Enter()
        {
            base.Enter();
            Creature.Rb2D.AddForce(new Vector2(Creature.XInput, _jumpForce), ForceMode2D.Impulse);
        }

        public override void Update()
        {
            base.Update();

            if (Creature.CollisionInfo.IsGrounded)
            {
                StateMachine.ChangeState(Creature.MoveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}