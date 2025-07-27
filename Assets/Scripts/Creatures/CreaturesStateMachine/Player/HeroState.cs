using Components.HealthComponentFolder;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroState : CreatureState
    {
        protected readonly Hero Hr;
        private readonly PlayerHealthComponent _healthComponent;
        public HeroState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
            Hr = hr;
            _healthComponent = Hr.GetComponent<PlayerHealthComponent>();
            _healthComponent.OnDeath += DeathHero;
        }

        ~HeroState()
        {
            _healthComponent.OnDeath -= DeathHero;
        }
        
        private void DeathHero()
        {
            Hr.StateMachine.ChangeState(Hr.DeathState);
        }
    }
}