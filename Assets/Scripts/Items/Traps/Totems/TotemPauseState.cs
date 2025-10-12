using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemPauseState : BasicState
    {
        private float _time;
        private float _duration = 2f;
        public TotemPauseState(BasicCreature creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();

            _time += Time.deltaTime;

            if (_time >= _duration)
            {
                StateMachine.ChangeState(Creature.AttackState);
                _time = 0;
            }
        }
    }
}