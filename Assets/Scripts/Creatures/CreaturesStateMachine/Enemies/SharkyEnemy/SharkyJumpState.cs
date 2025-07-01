using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyJumpState : CreatureState
    {
        private readonly Sharky _sharky;
        public SharkyJumpState(Sharky sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
            _sharky = sharky;
        }

        public override void Enter()
        {
            base.Enter();
            _sharky.Rb2D.AddForce(new Vector2(_sharky.XInput * 1.5f, _sharky.JumpForce), ForceMode2D.Impulse);
            _sharky.CallOnJumpEvent();
        }

        public override void Update()
        {
            base.Update();

            if (_sharky.CollisionInfo.IsGrounded)
            {
                StateMachine.ChangeState(_sharky.SharkyMoveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}