using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroState : BasicState
    {
        protected readonly Hero Hr;
        public HeroState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
            Hr = hr;
        }
    }
}