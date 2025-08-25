using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyHitState : EnemyState
    {
        private Vector2 _hitDirection;
        public EnemyHitState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemy, stateMachine, animBoolName)
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
            
            if (StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Hit)) &&
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
            }
        }

        public void SetDirection(Vector2 direction)
        {
            _hitDirection = direction;
        }
    }
}