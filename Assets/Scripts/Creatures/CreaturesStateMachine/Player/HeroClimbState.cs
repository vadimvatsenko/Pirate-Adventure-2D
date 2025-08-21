using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroClimbState : HeroState
    {
        public HeroClimbState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
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
                Debug.Log("Jump from Climb");
                Hr.StateMachine.ChangeState(Hr.JumpState);
            }

            /*if (Rb2D.velocity.y <= 0.1f)
            {   
                Hr.StateMachine.ChangeState(Hr.FallState);
            }*/
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}