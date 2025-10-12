using System.Collections;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroClimbState : HeroState
    {
        private bool _isSubscribed;
        private float _saveGravity;
        public HeroClimbState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            if (!_isSubscribed)
            {
                _isSubscribed = true;
                Hr.NewInputSet.Hero.Jump.started += ToJumpState;
                Hr.NewInputSet.Hero.Down.started += ToFallState;
                Hr.NewInputSet.Hero.Movement.started += ToFallState;
            }

            _saveGravity = Rb2D.gravityScale;
            Rb2D.gravityScale = 0f;
            Rb2D.velocity = Vector2.zero;
        }

        public override void Update()
        {
            Rb2D.velocity = Vector2.zero;
        }
        
        public override void Exit()
        {
            base.Exit();
            Rb2D.gravityScale = _saveGravity;
            if (_isSubscribed)
            {
                Hr.NewInputSet.Hero.Jump.started -= ToJumpState;
                Hr.NewInputSet.Hero.Down.started -= ToFallState;
                Hr.NewInputSet.Hero.Movement.started -= ToFallState;
                _isSubscribed = false;
            }
        }

        private void ToJumpState(InputAction.CallbackContext ctx) => StateMachine.ChangeState(Hr.JumpState);

        private void ToFallState(InputAction.CallbackContext ctx)
        {
            if (Rb2D.velocity.y <= 0f)
            {
                StateMachine.ChangeState(Hr.FallState);
            }
        }
        
    }
}