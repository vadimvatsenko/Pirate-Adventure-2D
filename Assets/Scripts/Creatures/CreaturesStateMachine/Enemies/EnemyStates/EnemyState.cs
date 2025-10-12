using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.EnemyStates
{
    public class EnemyState : BasicState
    {
        protected Enemy En;
        private Hero _hr;
        protected readonly EnemyCollisionInfo EnemyCollisionInfo;
        
        public EnemyState(Enemy en, BasicStateMachine stateMachine, int animBoolName) 
            : base(en, stateMachine, animBoolName)
        {
            En = en;
            StateMachine = stateMachine;
            
            if (En != null) EnemyCollisionInfo = En.EnemyCollisionInfo;

            //Health.SubscribeOnHitEvent(CallHitState);
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log($"Entered in {AnimatorHashes.GetName(_animBoolName)}");
        }

        ~EnemyState() => Health.UnsubscribeOnHitEvent(CallHitState);
        
        private void CallHitState() => StateMachine.ChangeState(En.HitState);
        
        protected float DistanceToHero()
        {
            _hr = En.Hr;
            if (_hr == null) return float.MaxValue;

            return Mathf.Abs(_hr.transform.position.x - En.transform.position.x);
        }

        protected int DirectionToHero()
        {
            if (_hr == null) return 0;
            else return _hr.transform.position.x > En.transform.position.x ? 1 : -1;
        }
    }
}