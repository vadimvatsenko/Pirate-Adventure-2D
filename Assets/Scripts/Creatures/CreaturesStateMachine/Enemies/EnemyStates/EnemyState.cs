using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Enemies.SharkyEnemy;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyState : CreatureState
    {
        protected readonly Enemy Enemy;
        private Hero Hr;
        protected readonly SharkyCollisionInfo CollisionInfo;
        
        public EnemyState(Enemy enemy, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemy, stateMachine, animBoolName)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
            
            if (Enemy != null) CollisionInfo = Enemy.SharkyCollisionInfo;

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
            Hr = Enemy.Hr;
            if (Hr == null) return float.MaxValue;

            return Mathf.Abs(Hr.transform.position.x - Enemy.transform.position.x);
        }

        protected int DirectionToHero()
        {
            if (Hr == null) return 0;
            else return Hr.transform.position.x > Enemy.transform.position.x ? 1 : -1;
        }
    }
}