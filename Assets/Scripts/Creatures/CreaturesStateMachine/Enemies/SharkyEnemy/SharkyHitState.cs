using Creatures.AnimationControllers;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyHitState : SharkyState
    {
        public SharkyHitState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = new Vector2(2 * -Sharky.FacingDirection, 2);
        }

        public override void Update()
        {
            base.Update();
            if (StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Hit))
                && StateInfo.normalizedTime > 1.0f
                )
            {
                if (Health.Health <= 0)
                {
                    StateMachine.ChangeState(Sharky.DeathState);
                }
                else if (CollisionInfo.IsGrounded)
                {
                    StateMachine.ChangeState(Sharky.BattleState);
                }
            }
        }
    }
}