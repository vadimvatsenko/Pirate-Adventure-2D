using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.Interfaces
{
    public interface IStateble
    {
        CreatureState IdleState {get; set;}
        CreatureState AttackState { get; set; }
        CreatureState HitState { get; set; }
        CreatureState DeathState { get; set; }
    }
}