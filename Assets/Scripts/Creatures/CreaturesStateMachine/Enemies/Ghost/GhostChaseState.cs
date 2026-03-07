using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostChaseState : GhostBaseState
    {
        public GhostChaseState(Ghost creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Ghost.IsChaising = true;
        }

        public override void Update()
        {
            base.Update();
            
            Ghost.IdleTimer -= Time.deltaTime;

            /*if (!Ghost.IsChaising && Ghost.IdleTimer < 0)
            {
                //Ghost.StateMachine.ChangeState(Ghost.ChaseState);
            }*/
            if (Ghost.IsChaising && Ghost.IdleTimer < 0)
            {
                StateMachine.ChangeState(Ghost.InvisibleState);
            }
            
        }
        
        
        public override void Exit()
        {
            base.Exit();
            Ghost.IdleTimer = Ghost.ActiveDuration;
            Ghost.IsChaising = false;
        }
    }
}