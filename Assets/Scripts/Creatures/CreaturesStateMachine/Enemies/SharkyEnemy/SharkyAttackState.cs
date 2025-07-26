using System.Linq;
using Components;
using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAttackState : SharkyState
    {
        public SharkyAttackState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //Attack();
            Rb2D.velocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)) && StateInfo.normalizedTime > 1.0f)
            {
                StateMachine.ChangeState(Sharky.BattleState);
                Sharky.CallOnAttackEvent();
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