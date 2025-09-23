using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyHitState : EnemyState
    {
        public EnemyHitState(Enemy en, CreatureStateMachine stateMachine, int animBoolName) 
            : base(en, stateMachine, animBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            //Rb2D.velocity = new Vector2(Enemy.Hit.x, Enemy.Hit.y);
        }

        public override void Update()
        {
            base.Update();
            
            /*if (StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Hit)) &&
                StateInfo.normalizedTime > 0.1f)
            {
                if (Health.Health <= 0)
                {
                    StateMachine.ChangeState(Enemy.DeathState);
                }
                else
                {
                    StateMachine.ChangeState(Enemy.BattleState);
                }
            }*/
        }
        
    }
}