using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyJumpState : SharkyAiredState
    {
        public SharkyJumpState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log(EnemySharky.Rb2D.velocity);
            EnemySharky.Rb2D.velocity = new Vector2(2.5f, 3f);
            Debug.Log(EnemySharky.Rb2D.velocity);
            EnemySharky.CallOnJumpEvent();
        }

        public override void Update()
        {
            base.Update();
            
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}