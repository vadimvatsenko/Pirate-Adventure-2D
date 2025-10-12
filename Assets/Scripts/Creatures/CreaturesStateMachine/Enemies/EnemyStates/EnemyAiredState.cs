using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyAiredState : EnemyState
    {
        public EnemyAiredState(Enemy en, BasicStateMachine stateMachine, int animBoolName) 
            : base(en, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            
            if (EnemyCollisionInfo.IsGrounded && Rb2D.velocity.y <= 0)
            {
                StateMachine.ChangeState(En.MoveState);
            }
        }
    }
}