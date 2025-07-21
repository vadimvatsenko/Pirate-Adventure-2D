using UnityEngine;
using UnityEngine.UIElements;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyMoveState : SharkyGroundedState
    {
        public SharkyMoveState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }
        
        public override void Enter()
        {
           base.Enter();
        }

        public override void Update()
        {
            base.Update();
            Sharky.HandleMovement();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}