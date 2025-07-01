using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroAiredState : HeroState
    {
        public int JumpCounter { get; protected set; }
        public HeroAiredState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
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
            
            if (Hero.XInput != 0)
            {
                Hero.Rb2D.velocity = new Vector2(Hero.XInput * (Hero.MovementSpeed * .8f), Hero.Rb2D.velocity.y);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
        
    }
}