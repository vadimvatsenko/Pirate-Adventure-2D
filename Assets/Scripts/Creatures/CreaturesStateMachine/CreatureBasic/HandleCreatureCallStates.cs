using UnityEditorInternal;

namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class HandleCreatureCallStates
    {
        private readonly Creature _creature;
        private readonly CreatureStateMachine _stateMachine;

        private HandleCreatureCallStates(Creature creature, CreatureStateMachine stateMachine)
        {
            this._creature = creature;
            this._stateMachine = stateMachine;
        }
        
    }
}