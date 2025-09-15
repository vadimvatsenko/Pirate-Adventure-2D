using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroGroundState : HeroState
    {
        public HeroGroundState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
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

            if (Hr.NewInputSet.Hero.Interact.triggered && CollisionInfo.IsGrounded)
            {
                CollisionInfo.Interact();
            }
        }
    }
}