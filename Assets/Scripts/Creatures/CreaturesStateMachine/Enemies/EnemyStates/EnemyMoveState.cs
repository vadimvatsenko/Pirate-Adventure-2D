using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyMoveState : EnemyGroundedState
    {
        public EnemyMoveState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemy, stateMachine, animBoolName)
        {
        }
        
        public override void Enter()
        {
           base.Enter();
        }

        public override void Update()
        {
            base.Update();
            Enemy.HandleMovement();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}