using UnityEditorInternal;

namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class CreatureHandleStateChange
    {
        private Creature _creature;
        private CreatureStateMachine _stateMachine;

        public CreatureHandleStateChange(Creature creature, CreatureStateMachine stateMachine)
        {
            _creature = creature;
            _stateMachine = stateMachine;
        }
        
        public void CallDeathState() => _stateMachine.ChangeState(_creature.DeathState);
    }
}