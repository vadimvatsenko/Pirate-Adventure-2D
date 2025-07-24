using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroDieState : HeroState
    {
        public HeroDieState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Rb2D.AddForce(new Vector2(0.5f * -Hr.FacingDirection, 2f), ForceMode2D.Impulse);
            Hr.NewInputSet.Disable();
            Rb2D.isKinematic = true;
            Rb2D.velocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}