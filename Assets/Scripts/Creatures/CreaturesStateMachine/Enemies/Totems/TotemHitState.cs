using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Creatures.CreaturesStateMachine.Enemies.Totems
{
    public class TotemHitState : TotemBasicState
    {
        public TotemHitState(TotemTrap creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Hit)) && StateInfo.normalizedTime >= 1.0f)
            {
                StateMachine.ChangeState(Creature.IdleState);
            }
        }
    }
}