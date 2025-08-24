using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Enemies.SharkyEnemy;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyBattleState : EnemyState
    {
        private Transform _heroPos;
        private float _lastTimeInBattle;

        public EnemyBattleState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName)
            : base(enemy, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (_heroPos == null) 
                _heroPos = CollisionInfo.HeroDetection().transform;
            
            if (ShouldRetreat())
            {
                Rb2D.velocity = new Vector2(Enemy.RetreatVelocity.x * -Enemy.FacingDirection, Rb2D.velocity.y);
            }
        }

        public override void Update()
        {
            base.Update();
            
            if (CollisionInfo.HeroDetection()) UpdateBattleTimer();
            
            if (BattleTimeIsOver())
            {
                Enemy.CallOnWTFEvent();
                StateMachine.ChangeState(Enemy.IdleState);
            }
            
            if (DirectionToHero() != Enemy.FacingDirection)
            {
                Enemy.HandleFlip();
            }
            
            if (WithinAttackRange() && CollisionInfo.HeroDetection())
            {
                StateMachine.ChangeState(Enemy.AttackState);
            }
            else
            {
                Rb2D.velocity = new Vector2(Enemy.BattleSpeed * Enemy.FacingDirection, Rb2D.velocity.y);
            }
            
            if (CollisionInfo.IsAbyssDetected)
            {
                Enemy.CallOnWTFEvent();
                StateMachine.ChangeState(Enemy.IdleState);
            }
        }
        
        private bool WithinAttackRange() => DistanceToHero() < CollisionInfo.AttackDistance;
        private bool ShouldRetreat() => DistanceToHero() < Enemy.MinRetreatDistance;

        // в Update постоянно записываем внутриигровое время
        private void UpdateBattleTimer() => _lastTimeInBattle = Time.time;
        private bool BattleTimeIsOver() => Time.time > _lastTimeInBattle + Enemy.BattleTimeDuration;
    }
}