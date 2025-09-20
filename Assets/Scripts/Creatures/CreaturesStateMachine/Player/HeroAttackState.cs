using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroAttackState : HeroState
    {
        public HeroAttackState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();

            if (CollisionInfo.IsGrounded)
            {
                Rb2D.velocity = Vector2.zero;
            }
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)) && StateInfo.normalizedTime > 1.0f)
            {
                StateMachine.ChangeState(Hr.IdleState);
            }
        }
    }
}