
namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class HandleCreatureCallStates
    {
        private readonly BasicCreature _creature;
        private readonly BasicStateMachine _stateMachine;

        private HandleCreatureCallStates(Creature creature, BasicStateMachine stateMachine)
        {
            this._creature = creature;
            this._stateMachine = stateMachine;
        }
        
    }
}