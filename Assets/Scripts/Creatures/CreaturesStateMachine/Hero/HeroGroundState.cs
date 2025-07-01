using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroGroundState : HeroState
    {
        public HeroGroundState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
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

            if (Hero.Rb2D.velocity.y < 0 && !Hero.CollisionInfo.IsGrounded)
            {
                StateMachine.ChangeState(Hero.HeroFallState);
            }
            
            if (Hero.NewInputSet.Hero.Jump.triggered && Hero.CollisionInfo.IsGrounded)
            {
                Hero.StateMachine.ChangeState(Hero.HeroJumpState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}