using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyDeathState : SharkyState
    {
        public SharkyDeathState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //Rb2D.velocity = new Vector2(2 * -Sharky.FacingDirection, 2);
            Rb2D.AddForce(new Vector2(2 * -Sharky.FacingDirection, 3), ForceMode2D.Impulse);
            Health.enabled = false;
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