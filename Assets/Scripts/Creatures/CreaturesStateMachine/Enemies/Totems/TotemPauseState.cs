using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Totems
{
    public class TotemPauseState : TotemBasicState
    {
        private float _time;
        private float _duration = 2f;
        public TotemPauseState(TotemTrap creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();

            if (!TotemCollisionInfo.HeroDetect)
            {
                StateMachine.ChangeState(Creature.IdleState);
                return;
            }

            _time += Time.deltaTime;

            if (_time >= _duration)
            {
                StateMachine.ChangeState(Creature.AttackState);
                _time = 0;
            }
        }
    }
}