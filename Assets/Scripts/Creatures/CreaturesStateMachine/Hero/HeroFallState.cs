using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroFallState : HeroAiredState
    {
        public HeroFallState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            
            if (Hero.CollisionInfo.IsGrounded && Hero.Rb2D.velocity.y <= 0.1f)
            {
                StateMachine.ChangeState(Hero.HeroIdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}