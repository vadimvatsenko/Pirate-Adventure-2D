using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TrapIdleState : TotemBasicState
    {
        public TrapIdleState(TotemTrap creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            
            if(!TotemCollisionInfo.HeroDetect) return;
            
            /*if (TotemCollisionInfo.HeroDetect)
            {
                StateMachine.ChangeState(Creature.AttackState);
            }*/
        }
    }
}