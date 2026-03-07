using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostBaseState : BasicState
    {
        protected readonly Ghost Ghost;
        public GhostBaseState(Ghost creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
            Ghost = creature;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}