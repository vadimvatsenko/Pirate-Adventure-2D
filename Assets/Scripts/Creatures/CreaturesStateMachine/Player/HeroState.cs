using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroState : CreatureState
    {
        protected readonly Hero Hr;
        public HeroState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
            Hr = hr;
        }
    }
}