using Animation;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostInvisibleState: GhostBaseState
    {
        public GhostInvisibleState(Ghost creature, BasicStateMachine stateMachine, int animBoolName) : base(creature, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            Debug.Log("Entering... in InvisibleState");
            //MakeInvisible();
        }

        public override void Update()
        {
            if (Ghost.IsHeroDetection)
            {
                Debug.Log("Ghost detected");
                Ghost.StateMachine.ChangeState(Ghost.AppearState);
            }
        }
    }
}