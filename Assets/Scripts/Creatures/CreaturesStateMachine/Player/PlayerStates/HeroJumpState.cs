using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player.PlayerStates;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroJumpState : HeroAiredState
    {
        public HeroJumpState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = new Vector2(Hr.Rb2D.velocity.x, Hr.JumpForce);
            
            Hr.CallOnJumpEvent();
        }

        public override void Update()
        {
            base.Update();
            
            if (Hr.Rb2D.velocity.y < 0)
            {
                StateMachine.ChangeState(Hr.FallState);
            }
        }
    }
}