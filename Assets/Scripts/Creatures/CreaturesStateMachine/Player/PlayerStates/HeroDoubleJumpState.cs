using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.PlayerStates
{
    public class HeroDoubleJumpState : HeroAiredState
    {
        public HeroDoubleJumpState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            Debug.Log("HeroDoubleJumpState Enter");
            base.Enter();
            Rb2D.AddForce(new Vector2(Rb2D.velocity.x, Hr.DoubleJumpForce), ForceMode2D.Impulse);
        }

        public override void Update()
        {
            base.Update();
            
            if (Rb2D.velocity.y < 0)
            {
                StateMachine.ChangeState(Hr.FallState);
            }
        }
    }
}