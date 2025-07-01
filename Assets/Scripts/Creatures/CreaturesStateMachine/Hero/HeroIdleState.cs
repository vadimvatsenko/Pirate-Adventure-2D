using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroIdleState : HeroGroundState
    {
        public HeroIdleState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Hero.Rb2D.velocity = new Vector2(0, Hero.Rb2D.velocity.y);
        }

        public override void Update()
        {
            base.Update();
            
            if (Hero.XInput != 0)
            {
                Hero.StateMachine.ChangeState(Hero.HeroMoveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}