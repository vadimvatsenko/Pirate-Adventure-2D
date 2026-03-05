using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostBaseState : BasicState
    {
        protected Ghost Ghost;
        public GhostBaseState(Ghost creature, BasicStateMachine stateMachine, int animBoolName) : base(creature, stateMachine, animBoolName)
        {
            Ghost = creature;
        }
    }
}