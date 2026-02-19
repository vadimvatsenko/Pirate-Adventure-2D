using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroMoveState : HeroGroundState
    {
        
        public HeroMoveState(Hero hr, BasicStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = new Vector2(Hr.XInput * Hr.MovementSpeed, Rb2D.velocity.y);
        }

        public override void Update()
        {
            base.Update();
            
            if (Hr.XInput == 0)
            {
                StateMachine.ChangeState(Hr.IdleState);
            }
            Rb2D.velocity = new Vector2(Hr.XInput * Hr.MovementSpeed, Hr.Rb2D.velocity.y);
        }
    }
}