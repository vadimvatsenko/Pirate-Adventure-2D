using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroHitState : HeroState
    {
        public HeroHitState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = new Vector2(5f * -Hr.FacingDirection, 5f);
        }

        public override void Update()
        {
            base.Update();
            if (CollisionInfo.IsGrounded)
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