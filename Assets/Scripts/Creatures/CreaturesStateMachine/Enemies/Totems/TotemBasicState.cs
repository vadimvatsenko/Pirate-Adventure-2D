using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.Totems
{
    public class TotemBasicState : BasicState
    {
        protected readonly TotemCollisionInfo TotemCollisionInfo;
        public TotemBasicState(TotemTrap creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
            TotemCollisionInfo = Creature.GetComponentInParent<TotemCollisionInfo>();
        }
    }
}