using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyGroundedState : EnemyState
    {
        public EnemyGroundedState(Enemy en, CreatureStateMachine stateMachine, int animBoolName) 
            : base(en, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            
            /*if (Sharky.SharkyCollisionInfo.IsGroundAfterAbyssDetected && Sharky.SharkyCollisionInfo.IsAbyssDetected)
            {
                Sharky.StateMachine.ChangeState(Sharky.JumpState);
            }*/

            if (En.EnemyCollisionInfo.IsWallDetected 
                || En.EnemyCollisionInfo.IsAbyssDetected)
            {
                Rb2D.velocity = Vector2.zero;
                En.HandleFlip();
                StateMachine.ChangeState(En.IdleState);
            } 
            
            else if (EnemyCollisionInfo.HeroDetection())
            {
                //StateMachine.ChangeState(Sharky.BattleState);
                StateMachine.ChangeState(En.AggroState);
            }
        }
    }
}