using System;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

// состояние броска
namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroThrowState : HeroState
    {
        private float _currentGravity;
        public HeroThrowState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _currentGravity = Rb2D.gravityScale;
            Rb2D.velocity = Vector2.zero;
            Rb2D.gravityScale = 0f;
        }

        public override void Update()
        {
            base.Update();
            
            if (Mathf.Approximately(AnimContr.GetFloat(AnimatorHashes.ThrowTrigger), 1))
            {
                if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Throw)) 
               
                   && StateInfo.normalizedTime >= 1f)
                {
                    StateMachine.ChangeState(Hr.IdleState);
                    Debug.Log("Idle State");
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            Rb2D.gravityScale = _currentGravity;
        }
    }
}