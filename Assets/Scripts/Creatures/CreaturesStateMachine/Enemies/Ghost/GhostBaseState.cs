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
            /*Ghost.IdleTimer -= Time.deltaTime;

            if (!Ghost.IsChaising && Ghost.IdleTimer < 0)
            {
                //Ghost.StateMachine.ChangeState(Ghost.ChaseState);
            }
            else if (Ghost.IsChaising && Ghost.IdleTimer < 0)
            {
                EndChase();
            }*/
        }
        
        protected void EndChase()
        {
            Ghost.IdleTimer = Ghost.ActiveDuration;
            Ghost.IsChaising = false;
        }

        protected void MakeInvisible()
        {
            Ghost.SpriteRenderer.color = Color.clear;
            Ghost.Collider.enabled = false;
        }

        protected void MakeVisible()
        {
            Ghost.SpriteRenderer.color = Color.white;
            Ghost.Collider.enabled = true;
        }
    }
}