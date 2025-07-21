using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroDieState : HeroState
    {
        public HeroDieState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Hero.Rb2D.AddForce(new Vector2(0.5f * Hero.FacingDirection, 2f), ForceMode2D.Impulse);
            Hero.NewInputSet.Disable();
            Hero.Rb2D.isKinematic = true;
            
            Hero.Rb2D.velocity = Vector2.zero;
            
            
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}