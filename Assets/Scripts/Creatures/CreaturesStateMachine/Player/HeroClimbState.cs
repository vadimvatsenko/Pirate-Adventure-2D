using System.Collections;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroClimbState : HeroState
    {
        private readonly BoxCollider2D _climbingBox;
        public HeroClimbState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
            _climbingBox = Hr.ClimbingBox;
        }

        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = Vector2.zero;
        }
        
        
        public override void Update()
        {
            base.Update();
            if (Hr.NewInputSet.Hero.Jump.triggered)
            {
                _climbingBox.enabled = false;
                StateMachine.ChangeState(Hr.JumpState);
            }

            if (Hr.NewInputSet.Hero.Down.triggered 
                || Hr.NewInputSet.Hero.Movement.triggered)
            {
                _climbingBox.enabled = false;
                StateMachine.ChangeState(Hr.FallState);
            }
        }
    }
}