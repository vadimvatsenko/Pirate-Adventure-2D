namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyFallState : SharkyAiredState
    {
        public SharkyFallState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
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