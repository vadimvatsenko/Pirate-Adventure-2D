using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyBattleState : SharkyState
    {
        private Transform _heroPos;
        public SharkyBattleState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            EnemySharky.Rb2D.velocity = new Vector2(EnemySharky.BattleSpeed * DirectionToPlayer(), EnemySharky.Rb2D.velocity.y);
            if (_heroPos == null)
            {
                _heroPos = EnemySharky.SharkyCollisionInfo.HeroDetection().transform;
            }
        }

        public override void Update()
        {
            base.Update();
            
            if(WithinAttackRange()) 
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyAttackState);
            
            else
            {
                EnemySharky.Rb2D.velocity 
                    = new Vector2(EnemySharky.BattleSpeed * DirectionToPlayer(), EnemySharky.Rb2D.velocity.y);
            }

            if (EnemySharky.SharkyCollisionInfo.IsAbyssDetected)
            {
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyIdleState);
            }
        }
        
        protected bool WithinAttackRange() => DistanceToHero() < EnemySharky.SharkyCollisionInfo.AttackDistance;

        private float DistanceToHero()
        {
            if(_heroPos == null) return float.MaxValue;
            
            return Mathf.Abs(_heroPos.position.x - EnemySharky.transform.position.x);
        }

        private int DirectionToPlayer()
        {
            if (_heroPos == null) return 0;
            
            return _heroPos.position.x > EnemySharky.transform.position.x ? 1 : -1;
        }
    }
}