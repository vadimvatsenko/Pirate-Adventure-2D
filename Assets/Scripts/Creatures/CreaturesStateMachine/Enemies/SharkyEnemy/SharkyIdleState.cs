using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyIdleState : CreatureIdleState
    {
        private float _idleTime;
        private float _idleDuration;
        
        public SharkyIdleState(Creature creature, CreatureStateMachine stateMachine, int animBoolName, float idleTime) 
            : base(creature, stateMachine, animBoolName)
        {
            _idleTime = idleTime;
        }
        public override void Enter()
        {
            base.Enter();
            _idleDuration = _idleTime;
            Creature.SetDirection(0);
        }

        public override void Update()
        {
            base.Update();
            _idleDuration -= Time.deltaTime;

            if (_idleDuration <= 0)
            {
                Creature.SetDirection(Creature.FacingDirection);
                StateMachine.ChangeState(Creature.MoveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}