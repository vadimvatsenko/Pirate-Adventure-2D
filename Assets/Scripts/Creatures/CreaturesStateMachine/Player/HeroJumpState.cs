using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroJumpState : HeroAiredState
    {
        public HeroJumpState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //Rb2D.velocity = new Vector2(Hr.Rb2D.velocity.x, Hr.JumpForce);
            //Rb2D.velocity = new Vector2(10, Hr.JumpForce);
            Rb2D.AddForce(new Vector2(20, 10), ForceMode2D.Impulse);
            
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