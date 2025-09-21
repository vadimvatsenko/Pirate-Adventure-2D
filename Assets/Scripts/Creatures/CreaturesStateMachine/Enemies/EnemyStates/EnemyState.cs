using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyState : CreatureState
    {
        protected readonly Enemy Enemy;
        private Hero _hr;
        protected readonly EnemyCollisionInfo EnemyCollisionInfo;
        
        public EnemyState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemy, stateMachine, animBoolName)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
            
            if (Enemy != null) EnemyCollisionInfo = Enemy.EnemyCollisionInfo;

            //Health.SubscribeOnHitEvent(CallHitState);
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log($"Entered in {AnimatorHashes.GetName(_animBoolName)}");
        }

        ~EnemyState() => Health.UnsubscribeOnHitEvent(CallHitState);
        
        private void CallHitState() => StateMachine.ChangeState(Enemy.HitState);
        
        protected float DistanceToHero()
        {
            _hr = Enemy.Hr;
            if (_hr == null) return float.MaxValue;

            return Mathf.Abs(_hr.transform.position.x - Enemy.transform.position.x);
        }

        protected int DirectionToHero()
        {
            if (_hr == null) return 0;
            else return _hr.transform.position.x > Enemy.transform.position.x ? 1 : -1;
        }
    }
}