using System;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemAttackState : TotemBasicState
    {
        public TotemAttackState(TotemTrap creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();

            if (!TotemCollisionInfo.HeroDetect)
            {
                StateMachine.ChangeState(Creature.IdleState);
            }
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)) && StateInfo.normalizedTime >= 1.0f)
            {
                if (TotemCollisionInfo.HeroDetect)
                {
                    StateMachine.ChangeState(Creature.PauseState);
                }
            }
        }
    }
}