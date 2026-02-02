using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemHitState : TotemBasicState
    {
        public TotemHitState(TotemTrap creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Hit)) && StateInfo.normalizedTime >= 1.0f)
            {
                StateMachine.ChangeState(Creature.IdleState);
            }
        }
    }
}