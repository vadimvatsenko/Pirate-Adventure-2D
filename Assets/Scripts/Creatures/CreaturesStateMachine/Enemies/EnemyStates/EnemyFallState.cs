using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyFallState : EnemyAiredState
    {
        public EnemyFallState(Enemy en, CreatureStateMachine stateMachine, int animBoolName) 
            : base(en, stateMachine, animBoolName)
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