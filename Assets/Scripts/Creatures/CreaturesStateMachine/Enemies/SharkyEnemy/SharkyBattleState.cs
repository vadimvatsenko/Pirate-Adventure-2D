using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyBattleState : SharkyState
    {
        private Transform _heroPos;
        private float _lastTimeInBattle;

        public SharkyBattleState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName)
            : base(sharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (_heroPos == null) _heroPos = CollisionInfo.HeroDetection().transform;
            
            if (ShouldRetreat())
            {
                Rb2D.velocity = new Vector2(Sharky.RetreatVelocity.x * -Sharky.FacingDirection, Rb2D.velocity.y);
            }
        }

        public override void Update()
        {
            base.Update();
            
            if (CollisionInfo.HeroDetection()) UpdateBattleTimer();
            
            if (BattleTimeIsOver())
            {
                Sharky.CallOnWTFEvent();
                StateMachine.ChangeState(Sharky.IdleState);
            }
            
            if (DirectionToHero() != Sharky.FacingDirection)
            {
                Sharky.HandleFlip();
            }
            
            if (WithinAttackRange() && CollisionInfo.HeroDetection())
            {
                Debug.Log("Enemies are attacking");
                StateMachine.ChangeState(Sharky.AttackState);
            }
            else
            {
                Rb2D.velocity = new Vector2(Sharky.BattleSpeed * Sharky.FacingDirection, Rb2D.velocity.y);
            }
            
            if (CollisionInfo.IsAbyssDetected)
            {
                Sharky.CallOnWTFEvent();
                StateMachine.ChangeState(Sharky.IdleState);
            }
        }
        
        private bool WithinAttackRange() => DistanceToHero() < CollisionInfo.AttackDistance;
        private bool ShouldRetreat() => DistanceToHero() < Sharky.MinRetreatDistance;

        // в Update постоянно записываем внутриигровое время
        private void UpdateBattleTimer() => _lastTimeInBattle = Time.time;
        private bool BattleTimeIsOver() => Time.time > _lastTimeInBattle + Sharky.BattleTimeDuration;
    }
}