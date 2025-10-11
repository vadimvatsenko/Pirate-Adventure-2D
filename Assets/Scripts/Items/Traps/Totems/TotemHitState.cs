using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Items.Traps.Totems
{
    public class TotemHitState : CreatureState
    {
        public TotemHitState(BasicCreature creature, CreatureStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}