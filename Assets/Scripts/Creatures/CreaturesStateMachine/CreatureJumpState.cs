namespace Creatures.CreaturesStateMachine
{
    public class CreatureJumpState : CreatureState
    {
        public CreatureJumpState(Creature creature, CreatureStateMachine stateMachine, int animBoolName) 
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