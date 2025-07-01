using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroState : CreatureState
    {
        protected Hero Hero;
        public HeroState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
            Hero = hero;
        }

        public override void Enter()
        {
            base.Enter();
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