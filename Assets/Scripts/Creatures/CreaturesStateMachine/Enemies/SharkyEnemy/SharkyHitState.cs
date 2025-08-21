using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
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
            
            if (StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Hit)) &&
                StateInfo.normalizedTime > 0.1f)
            {
                if (Health.Health <= 0)
                {
                    StateMachine.ChangeState(Sharky.DeathState);
                }
                else
                {
                    StateMachine.ChangeState(Sharky.BattleState);
                }
            }
        }
    }
}