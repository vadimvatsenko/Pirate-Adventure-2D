using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Player.PlayerStates
{
    public class HeroState : BasicState
    {
        protected readonly Hero Hr;
        public HeroState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
            Hr = hr;
        }
    }
}