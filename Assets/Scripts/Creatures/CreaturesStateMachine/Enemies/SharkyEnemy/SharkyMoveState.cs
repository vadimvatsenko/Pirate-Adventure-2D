using UnityEngine;
using UnityEngine.UIElements;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyMoveState : SharkyGroundedState
    {
        public SharkyMoveState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
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
            EnemySharky.HandleMovement();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}