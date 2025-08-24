using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Enemies.SharkyEnemy;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyGroundedState : EnemyState
    {
        public EnemyGroundedState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemy, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            
            /*if (Sharky.SharkyCollisionInfo.IsGroundAfterAbyssDetected && Sharky.SharkyCollisionInfo.IsAbyssDetected)
            {
                Sharky.StateMachine.ChangeState(Sharky.JumpState);
            }*/

            if (Enemy.SharkyCollisionInfo.IsWallDetected 
                || Enemy.SharkyCollisionInfo.IsAbyssDetected)
            {
                Rb2D.velocity = Vector2.zero;
                Enemy.HandleFlip();
                StateMachine.ChangeState(Enemy.IdleState);
            } 
            
            else if (CollisionInfo.HeroDetection())
            {
                //StateMachine.ChangeState(Sharky.BattleState);
                StateMachine.ChangeState(Enemy.AggroState);
            }
        }
    }
}