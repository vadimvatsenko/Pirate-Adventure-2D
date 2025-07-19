using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyGroundedState : SharkyState
    {
        public SharkyGroundedState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            
            if (Creature.CollisionInfo.IsGroundAfterAbyssDetected && Creature.CollisionInfo.IsAbyssDetected)
            {
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyJumpState);
            }

            else if (Creature.CollisionInfo.IsWallDetected || Creature.CollisionInfo.IsAbyssDetected)
            {
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyIdleState);
                EnemySharky.HandleFlip();
            }
            
            else if (EnemySharky.SharkyCollisionInfo.IsHeroDetected)
            {
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyAggroState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}