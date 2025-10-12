using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Items.Traps.Totems
{
    public class TrapBattleState : BasicState
    {
        public TrapBattleState(BasicCreature creature, BasicStateMachine stateMachine, int animBoolName) 
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