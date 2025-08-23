using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Enemies.SharkyEnemy;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyAiredState : EnemyState
    {
        public EnemyAiredState(Enemy sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            
            if (CollisionInfo.IsGrounded && Rb2D.velocity.y <= 0)
            {
                StateMachine.ChangeState(Sharky.MoveState);
            }
        }
    }
}