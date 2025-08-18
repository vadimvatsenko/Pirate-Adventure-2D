namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroGroundState : HeroState
    {
        public HeroGroundState(Player.Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();

            if (Rb2D.velocity.y < 0.1f && !CollisionInfo.IsGrounded)
            {
                StateMachine.ChangeState(Hr.FallState);
            }
            
            // система ввода
            if (Hr.NewInputSet.Hero.Jump.triggered && CollisionInfo.IsGrounded)
            {
                StateMachine.ChangeState(Hr.JumpState);
            }

            // система ввода
            if (Hr.NewInputSet.Hero.Attack.triggered && CollisionInfo.IsGrounded && Hr.GameSess.PlayerData.isArmed)
            {
                StateMachine.ChangeState(Hr.AttackState);
            }
        }
    }
}