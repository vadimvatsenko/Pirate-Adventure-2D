using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostIdleState : GhostBaseState
    {
        public GhostIdleState(Ghost creature, BasicStateMachine stateMachine, int animBoolName) : base(creature, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}