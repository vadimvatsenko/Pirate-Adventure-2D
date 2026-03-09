using Animation;
using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostDisappearState : GhostBaseState
    {
        public GhostDisappearState(Ghost creature, BasicStateMachine stateMachine, int animBoolName) : base(creature, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Disappear)) && StateInfo.normalizedTime > 1.0f)
            {
                StateMachine.ChangeState(Ghost.InvisibleState);
            }
        }
    }
}