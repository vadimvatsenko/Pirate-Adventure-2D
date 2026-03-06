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
            StartChase();
        }
        
        private void StartChase()
        {
            Transform heroTransform = Ghost.GetHero().transform;
            
            if (heroTransform == null)
            {
                EndChase();
                return;
            }

            float xOffset = Random.Range(0, 100) < 50 ? -1 : 1;
            
            float yPos = Random.Range(Ghost.YMinDistance, Ghost.YMaxDistance);
            
            Ghost.transform.position = heroTransform.position + new Vector3(Ghost.XMinDistance * xOffset, yPos, 0);
            
            Ghost.ActiveTimer = Ghost.ActiveDuration;
            
            Ghost.IsChaising = true;
        }
    }
}