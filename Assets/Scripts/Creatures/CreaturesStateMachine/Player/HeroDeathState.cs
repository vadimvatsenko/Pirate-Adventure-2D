using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroDeathState : HeroState
    {
        private bool _isDead;
        public HeroDeathState(Hero hr, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hr, stateMachine, animBoolName)
        {
            _isDead = false;
        }

        public override void Enter()
        {
            base.Enter();

            if (!_isDead)
            {
                Rb2D.AddForce(new Vector2(5f * -Hr.FacingDirection, 6f), ForceMode2D.Impulse);
                Hr.NewInputSet.Disable();
                //Rb2D.isKinematic = true;
                //Rb2D.velocity = Vector2.zero;
                Hr.CallOnDeathEvent();
                _isDead = true;
                Hr.GameMg.ReloadLevel();
            }
        }
    }
}