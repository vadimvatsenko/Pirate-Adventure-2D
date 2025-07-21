using System.Linq;
using Components;
using Components.HealthComponentFolder;
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
            //Rb2D.velocity = Vector2.zero;
            Sharky.CallOnAttackEvent();
        }

        public override void Update()
        {
            base.Update();
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