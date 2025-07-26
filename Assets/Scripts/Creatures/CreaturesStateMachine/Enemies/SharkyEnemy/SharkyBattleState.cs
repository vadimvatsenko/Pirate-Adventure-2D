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

            Rb2D.velocity = new Vector2(Sharky.BattleSpeed * DirectionToPlayer(), Rb2D.velocity.y);
            
            if (_heroPos == null) _heroPos = CollisionInfo.HeroDetection().transform;

            if (ShouldRetreat())
            {
                Rb2D.velocity = new Vector2(Sharky.RetreatVelocity.x * -DirectionToPlayer(), Sharky.RetreatVelocity.y);
                //Debug.Log(Rb2D.velocity);
                //Sharky.HandleFlip();
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
            
            if (WithinAttackRange() && CollisionInfo.HeroDetection())
            {
                StateMachine.ChangeState(Sharky.AttackState);
            }
            
            else
            {
                if (DirectionToPlayer() != Sharky.FacingDirection) Sharky.HandleFlip();

                Sharky.Rb2D.velocity 
                    = new Vector2(Sharky.BattleSpeed * DirectionToPlayer(), Rb2D.velocity.y);
            }

            if (CollisionInfo.IsAbyssDetected)
            {
                Sharky.CallOnWTFEvent();
                StateMachine.ChangeState(Sharky.IdleState);
            }
        }
        
        protected bool WithinAttackRange() => DistanceToHero() < CollisionInfo.AttackDistance;

        private float DistanceToHero()
        {
            if(_heroPos == null) return float.MaxValue;
            
            return Mathf.Abs(_heroPos.position.x - Sharky.transform.position.x);
        }

        private int DirectionToPlayer()
        {
            if (_heroPos == null)
            {
                return 0;
            }
            else
            {
                return _heroPos.position.x > Sharky.transform.position.x ? 1 : -1;
            }
            
        }

        // в Update постоянно записываем внутриигровое время
        private void UpdateBattleTimer() => _lastTimeInBattle = Time.time;
        private bool BattleTimeIsOver() => Time.time > _lastTimeInBattle + Sharky.BattleTimeDuration;
        private bool ShouldRetreat() => DistanceToHero() < Sharky.MinRetreatDistance;
    }
}