using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroIdleState : HeroGroundState
    {
        public HeroIdleState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = new Vector2(0, Hr.Rb2D.velocity.y);
        }

        public override void Update()
        {
            base.Update();
            
            if (Hr.XInput != 0)
            {
                StateMachine.ChangeState(Hr.MoveState);
            }
        }
    }
}