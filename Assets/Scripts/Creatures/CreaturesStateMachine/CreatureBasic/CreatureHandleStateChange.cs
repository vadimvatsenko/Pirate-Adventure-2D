using UnityEditorInternal;

namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class CreatureHandleStateChange
    {
        private Creature _creature;
        private BasicStateMachine _stateMachine;

        public CreatureHandleStateChange(Creature creature, BasicStateMachine stateMachine)
        {
            _creature = creature;
            _stateMachine = stateMachine;
        }
        
        public void CallDeathState() => _stateMachine.ChangeState(_creature.DeathState);
    }
}