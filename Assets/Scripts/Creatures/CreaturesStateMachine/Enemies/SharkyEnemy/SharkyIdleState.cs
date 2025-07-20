using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyIdleState : SharkyGroundedState
    {
        private float _idleTime;
        private float _idleDuration;
        
        public SharkyIdleState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _idleDuration = EnemySharky.IdleDuration;
            EnemySharky.Rb2D.velocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
            _idleDuration -= Time.deltaTime;

            if (_idleDuration <= 0)
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