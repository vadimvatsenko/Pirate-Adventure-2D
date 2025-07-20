namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAiredState : SharkyState
    {
        public SharkyAiredState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            
            if (EnemySharky.CollisionInfo.IsGrounded && EnemySharky.Rb2D.velocity.y <= 0)
            {
                StateMachine.ChangeState(EnemySharky.SharkyMoveState);
            }
        }
    }
}