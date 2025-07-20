using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyGroundedState : SharkyState
    {
        public SharkyGroundedState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();

            
            RaycastHit2D hit = EnemySharky.SharkyCollisionInfo.HeroDetection();
            //Debug.Log(hit.transform);
            
            if (EnemySharky.SharkyCollisionInfo.IsGroundAfterAbyssDetected && EnemySharky.SharkyCollisionInfo.IsAbyssDetected)
            {
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyJumpState);
            }

            else if (EnemySharky.SharkyCollisionInfo.IsWallDetected || EnemySharky.SharkyCollisionInfo.IsAbyssDetected)
            {
                EnemySharky.Rb2D.velocity = Vector2.zero;
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyIdleState);
                EnemySharky.HandleFlip();
            } 
            
            else if (EnemySharky.SharkyCollisionInfo.HeroDetection())
            {
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyBattleState);
            }
        }
    }
}