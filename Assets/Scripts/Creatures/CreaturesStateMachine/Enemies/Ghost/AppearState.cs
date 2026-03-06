using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class AppearState : GhostBaseState
    {
        public AppearState(Ghost ghost, BasicStateMachine stateMachine, int animBoolName) : base(ghost, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            MakeVisible();
        }
    }
}