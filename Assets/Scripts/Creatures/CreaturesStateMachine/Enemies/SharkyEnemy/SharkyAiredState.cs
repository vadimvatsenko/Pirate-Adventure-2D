namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAiredState : SharkyState
    {
        public SharkyAiredState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
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