using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.PlayerStates
{
    public class HeroJumpState : HeroAiredState
    {
        public HeroJumpState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = new Vector2(Hr.Rb2D.velocity.x, Hr.JumpForce);
            Hr.JumpCounter++;
            Hr.CallOnJumpEvent();
        }

        public override void Update()
        {
            base.Update();

            if (Hr.NewInputSet.Hero.Jump.triggered && Hr.JumpCounter == 1)
            {
                Rb2D.velocity = new Vector2(Hr.Rb2D.velocity.x, Hr.DoubleJumpForce);
                Hr.JumpCounter++;
            }
            
            if (Hr.Rb2D.velocity.y < 0)
            {
                StateMachine.ChangeState(Hr.FallState);
            }
        }
    }
}