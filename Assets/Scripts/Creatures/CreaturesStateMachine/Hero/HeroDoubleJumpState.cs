using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroDoubleJumpState : HeroAiredState
    {
        public HeroDoubleJumpState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Hero.Rb2D.velocity = new Vector2(Hero.Rb2D.velocity.x, Hero.DoubleJumpForce);
        }

        public override void Update()
        {
            base.Update();
            
            if (Hero.Rb2D.velocity.y < 0)
            {
                Hero.StateMachine.ChangeState(Hero.HeroFallState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}