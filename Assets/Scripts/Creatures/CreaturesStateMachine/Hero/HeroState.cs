using Components.HealthComponentFolder;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroState : CreatureState
    {
        protected Hero Hero;
        private readonly PlayerHealthComponent _healthComponent;
        public HeroState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
            Hero = hero;
            _healthComponent = Hero.GetComponent<PlayerHealthComponent>();
            _healthComponent.OnDeath += DeathHero;
        }

        ~HeroState()
        {
            _healthComponent.OnDeath -= DeathHero;
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

        private void DeathHero()
        {
            Hero.StateMachine.ChangeState(Hero.HeroDieState);
        }
    }
}