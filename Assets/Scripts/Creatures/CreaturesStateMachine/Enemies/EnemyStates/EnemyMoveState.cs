using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyMoveState : EnemyGroundedState
    {
        public EnemyMoveState(Enemy sharky, CreatureStateMachine stateMachine, int animBoolName) 
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
            Sharky.HandleMovement();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}