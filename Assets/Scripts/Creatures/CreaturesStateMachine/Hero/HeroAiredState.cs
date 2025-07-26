using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroAiredState : HeroState
    {
        public HeroAiredState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
        }
        public override void Update()
        {
            base.Update();
            
            if (Hr.XInput != 0)
            {
                Rb2D.velocity = new Vector2(Hr.XInput * (Hr.MovementSpeed * .8f), Rb2D.velocity.y);
            }
        }
    }
}