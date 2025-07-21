using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyIdleState : SharkyGroundedState
    {
        private float _idleTime;
        private float _idleDuration;
        
        public SharkyIdleState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _idleDuration = Sharky.IdleDuration;
            Rb2D.velocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
            _idleDuration -= Time.deltaTime;

            if (_idleDuration <= 0)
            {
                StateMachine.ChangeState(Sharky.MoveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}