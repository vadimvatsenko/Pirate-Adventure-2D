using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroAiredState : HeroState
    {
        public HeroAiredState(Player.Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
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