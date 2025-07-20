namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyFallState : SharkyAiredState
    {
        public SharkyFallState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
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