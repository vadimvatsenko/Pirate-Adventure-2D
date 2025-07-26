using Components;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyState : CreatureState
    {
        protected readonly SharkyE Sharky;
        protected readonly SharkyCollisionInfo CollisionInfo;
        
        public SharkyState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
            Sharky = sharky;
            StateMachine = stateMachine;
            
            if (Sharky != null) CollisionInfo = Sharky.SharkyCollisionInfo;

            Health.SubscribeOnHitEvent(CallHitState);
        }

        ~SharkyState() => Health.UnsubscribeOnHitEvent(CallHitState);
        
        public override void Update()
        {
            base.Update();
        }

        private void CallHitState() => StateMachine.ChangeState(Sharky.HitState);
        
        
    }
}