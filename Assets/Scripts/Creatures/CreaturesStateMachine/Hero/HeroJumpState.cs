using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroJumpState : HeroAiredState
    {
        public HeroJumpState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            JumpCounter++;
            Hero.Rb2D.velocity = new Vector2(Hero.Rb2D.velocity.x, Hero.JumpForce);
        }

        public override void Update()
        {
            base.Update();
            
            if (Hero.Rb2D.velocity.y < 0)
            {
                StateMachine.ChangeState(Hero.HeroFallState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}