using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyIdleState : CreatureState
    {
        private float _idleTime;
        private float _idleDuration;
        
        private readonly Sharky _sharky;
        
        public SharkyIdleState(Sharky sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
            _sharky = sharky;
        }
        public override void Enter()
        {
            base.Enter();
            _idleDuration = _sharky.IdleDuration;
            Creature.SetDirection(0);
        }

        public override void Update()
        {
            base.Update();
            _idleDuration -= Time.deltaTime;

            if (_idleDuration <= 0)
            {
                StateMachine.ChangeState(_sharky.SharkyMoveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}