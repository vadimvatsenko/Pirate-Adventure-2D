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
            Hero.Rb2D.AddForce(new Vector2(0.25f * Hero.FacingDirection, 0.5f), ForceMode2D.Impulse);
            Hero.NewInputSet.Disable();
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