using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyIdleState : EnemyGroundedState
    {
        private float _idleTime;
        private float _idleDuration;
        
        public EnemyIdleState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemy, stateMachine, animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _idleDuration = Enemy.IdleDuration;
            Rb2D.velocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
            _idleDuration -= Time.deltaTime;

            if (_idleDuration <= 0)
            {
                StateMachine.ChangeState(Enemy.MoveState);
            }
        }
    }
}