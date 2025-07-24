using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroJumpState : HeroAiredState
    {
        public HeroJumpState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = new Vector2(Hr.Rb2D.velocity.x, Hr.JumpForce);
        }

        public override void Update()
        {
            base.Update();
            
            if (Hr.Rb2D.velocity.y < 0)
            {
                StateMachine.ChangeState(Hr.FallState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}