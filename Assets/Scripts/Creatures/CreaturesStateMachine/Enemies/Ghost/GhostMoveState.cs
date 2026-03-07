using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostMoveState : GhostBaseState
    {
        public GhostMoveState(Ghost creature, BasicStateMachine stateMachine, int animBoolName) : base(creature, stateMachine, animBoolName)
        {
            
        }
    }
}