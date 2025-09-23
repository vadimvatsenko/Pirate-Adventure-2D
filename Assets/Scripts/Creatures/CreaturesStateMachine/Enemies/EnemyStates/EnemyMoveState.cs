using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyMoveState : EnemyGroundedState
    {
        public EnemyMoveState(Enemy en, CreatureStateMachine stateMachine, int animBoolName) 
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
            En.HandleMovement();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}