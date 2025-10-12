using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.Interfaces
{
    public interface IStateble
    {
        BasicState IdleState {get; set;}
        BasicState AttackState { get; set; }
        BasicState HitState { get; set; }
        BasicState DeathState { get; set; }
    }
}