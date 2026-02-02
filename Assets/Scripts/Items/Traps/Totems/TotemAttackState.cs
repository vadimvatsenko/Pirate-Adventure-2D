using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Items.Traps.Totems
{
    public class TotemAttackState : TotemBasicState
    {
        public TotemAttackState(TotemTrap creature, BasicStateMachine stateMachine, int animBoolName) 
            : base(creature, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)) && StateInfo.normalizedTime >= 1.0f)
            {
                
                if (TotemCollisionInfo.HeroAttack)
                {
                    StateMachine.ChangeState(Creature.PauseState);
                }
            }
        }
    }
}