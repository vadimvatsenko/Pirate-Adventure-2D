using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyBattleState : EnemyState
    {
        private Transform _heroPos;
        private float _lastTimeInBattle;

        public EnemyBattleState(Enemy en, BasicStateMachine stateMachine, int animBoolName)
            : base(en, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (_heroPos == null) 
                _heroPos = EnemyCollisionInfo.HeroDetection().transform;
            
            if (ShouldRetreat())
            {
                Rb2D.velocity = new Vector2(En.RetreatVelocity.x * -En.FacingDirection, Rb2D.velocity.y);
            }
        }

        public override void Update()
        {
            base.Update();
            
            if (EnemyCollisionInfo.HeroDetection()) UpdateBattleTimer();
            
            if (BattleTimeIsOver())
            {
                En.CallOnWTFEvent();
                StateMachine.ChangeState(En.IdleState);
            }
            
            if (DirectionToHero() != En.FacingDirection)
            {
                if (_heroPos != null)
                {
                    if(_heroPos.position.y > En.transform.position.y) return;
                    En.HandleFlip();
                }
            }
            
            if (WithinAttackRange() && EnemyCollisionInfo.HeroDetection())
            {
                StateMachine.ChangeState(En.AttackState);
            }
            else
            {
                Rb2D.velocity = new Vector2(En.BattleSpeed * En.FacingDirection, Rb2D.velocity.y);
            }
            
            if (EnemyCollisionInfo.IsAbyssDetected)
            {
                En.CallOnWTFEvent();
                StateMachine.ChangeState(En.IdleState);
            }
        }
        
        private bool WithinAttackRange() => DistanceToHero() < EnemyCollisionInfo.AttackDistance;
        private bool ShouldRetreat() => DistanceToHero() < En.MinRetreatDistance;

        // в Update постоянно записываем внутриигровое время
        private void UpdateBattleTimer() => _lastTimeInBattle = Time.time;
        private bool BattleTimeIsOver() => Time.time > _lastTimeInBattle + En.BattleTimeDuration;
    }
}