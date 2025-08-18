using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyGroundedState : SharkyState
    {
        public SharkyGroundedState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            
            /*if (Sharky.SharkyCollisionInfo.IsGroundAfterAbyssDetected && Sharky.SharkyCollisionInfo.IsAbyssDetected)
            {
                Sharky.StateMachine.ChangeState(Sharky.JumpState);
            }*/

            if (Sharky.SharkyCollisionInfo.IsWallDetected 
                || Sharky.SharkyCollisionInfo.IsAbyssDetected)
            {
                Rb2D.velocity = Vector2.zero;
                Sharky.HandleFlip();
                StateMachine.ChangeState(Sharky.IdleState);
            } 
            
            else if (CollisionInfo.HeroDetection())
            {
                //StateMachine.ChangeState(Sharky.BattleState);
                StateMachine.ChangeState(Sharky.AggroState);
            }
        }
    }
}