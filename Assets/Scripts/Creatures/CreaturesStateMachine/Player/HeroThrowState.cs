using System;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

// состояние броска
namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroThrowState : HeroState
    {
        
        public HeroThrowState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            Rb2D.velocity = Vector2.zero;
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Throw)) 
               
               && StateInfo.normalizedTime >= 1.0f)
            {
                StateMachine.ChangeState(Hr.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}