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
            
            /*if (DirectionToHero() != Sharky.FacingDirection)
            {
                Sharky.HandleFlip();
            }*/
            
            Rb2D.velocity = new Vector2(Sharky.Hit.x * Sharky.FacingDirection, Sharky.Hit.y);
        }

        public override void Update()
        {
            base.Update();
            
            if (StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Hit)))
            {
                if (Health.Health <= 0 && StateInfo.normalizedTime > 0.1f)
                {
                    StateMachine.ChangeState(Sharky.DeathState);
                }
                else if (CollisionInfo.IsGrounded && StateInfo.normalizedTime > 1.0f)
                {
                    StateMachine.ChangeState(Sharky.BattleState);
                }
            }
        }
    }
}