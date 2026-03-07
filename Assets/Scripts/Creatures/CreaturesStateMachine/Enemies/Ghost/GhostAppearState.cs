using System;
using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostAppearState : GhostBaseState
    {
        public GhostAppearState(Ghost ghost, BasicStateMachine stateMachine, int animBoolName) : base(ghost, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Console.WriteLine("Appearing...");
            MakeVisible();
        }
    }
}