using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyJumpState : SharkyState
    {
        public SharkyJumpState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            EnemySharky.SetDirection(2f * EnemySharky.FacingDirection);
            EnemySharky.Rb2D.velocity = new Vector2(EnemySharky.Rb2D.velocity.x, 3f);
            
            EnemySharky.CallOnJumpEvent();
        }

        public override void Update()
        {
            base.Update();
            
            Debug.Log(EnemySharky.Rb2D.velocity);

            if (EnemySharky.CollisionInfo.IsGrounded && EnemySharky.Rb2D.velocity.y < 0)
            {
                StateMachine.ChangeState(EnemySharky.SharkyMoveState);
                
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}