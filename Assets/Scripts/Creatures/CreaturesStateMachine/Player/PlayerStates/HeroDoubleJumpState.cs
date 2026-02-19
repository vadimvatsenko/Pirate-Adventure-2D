using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player.PlayerStates;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroDoubleJumpState : HeroAiredState
    {
        public HeroDoubleJumpState(Player.Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            //Rb2D.velocity = new Vector2(Rb2D.velocity.x, Hr.DoubleJumpForce);
        }

        public override void Update()
        {
            base.Update();
            
            if (Rb2D.velocity.y < 0)
            {
                StateMachine.ChangeState(Hr.FallState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}