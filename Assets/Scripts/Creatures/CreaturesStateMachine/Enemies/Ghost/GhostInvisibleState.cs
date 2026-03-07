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
            base.Enter();
            MakeInvisible();
        }

        public override void Update()
        {
            base.Update();
            
            if (Ghost.IsHeroDetection)
            {
                Ghost.StateMachine.ChangeState(Ghost.AppearState);
            }
        }
        
        private void MakeInvisible()
        {
            Ghost.SpriteRenderer.color = Color.clear;
            Ghost.C2D.enabled = false;
        }
    }
}