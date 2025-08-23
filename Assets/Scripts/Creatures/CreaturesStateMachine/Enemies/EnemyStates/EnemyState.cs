using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Enemies.SharkyEnemy;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyState : CreatureState
    {
        protected readonly Enemy Sharky;
        private Hero Hr;
        protected readonly SharkyCollisionInfo CollisionInfo;
        
        public EnemyState(Enemy sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
            Sharky = sharky;
            StateMachine = stateMachine;
            
            if (Sharky != null) CollisionInfo = Sharky.SharkyCollisionInfo;

            //Health.SubscribeOnHitEvent(CallHitState);
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log($"Entered in {AnimatorHashes.GetName(_animBoolName)}");
        }

        ~EnemyState() => Health.UnsubscribeOnHitEvent(CallHitState);
        
        private void CallHitState() => StateMachine.ChangeState(Sharky.HitState);
        
        protected float DistanceToHero()
        {
            Hr = Sharky.Hr;
            if (Hr == null) return float.MaxValue;

            return Mathf.Abs(Hr.transform.position.x - Sharky.transform.position.x);
        }

        protected int DirectionToHero()
        {
            if (Hr == null) return 0;
            else return Hr.transform.position.x > Sharky.transform.position.x ? 1 : -1;
        }
    }
}