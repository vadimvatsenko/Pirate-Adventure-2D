using System.Linq;
using Components;
using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAttackState : SharkyState
    {
        private AnimatorStateInfo _stateInfo;
        private string _animName;
        public SharkyAttackState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
            
        }

        public override void Enter()
        {
            base.Enter();

            _animName = AnimatorHashes.GetName(AnimatorHashes.Attack).ToString();
            
            Sharky.CallOnAttackEvent();
        }

        public override void Update()
        {
            base.Update();
            _stateInfo = AnimContr.GetCurrentAnimatorStateInfo(0);
            
            if(_stateInfo.IsName(_animName))
            {
                Debug.Log("Is same name");
            }
        }
        
        public void Attack()
        {
            if(!CollisionInfo.IsGrounded) return;
            
            var gos = CollisionInfo.GetObjectsInRange();
            
            foreach (var go in gos)
            {
                var hp = go.GetComponent<IHealthComponent>();
                if (hp != null)
                {
                    hp.ApplyDamage(1);
                }
            }
        }
    }
}