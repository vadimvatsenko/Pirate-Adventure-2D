using System;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemAttackState : BasicState
    {
        public TotemAttackState(BasicCreature creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enter Attack");
        }

        public override void Update()
        {
            base.Update();
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)) && StateInfo.normalizedTime >= 1.0f)
            {
                StateMachine.ChangeState(Creature.PauseState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exit Attack");
        }
    }
}