using System;
using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemBasicState : BasicState
    {
        protected readonly TotemCollisionInfo TotemCollisionInfo;
        public TotemBasicState(TotemTrap creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
            TotemCollisionInfo = Creature.GetComponentInParent<TotemCollisionInfo>();
        }
    }
}