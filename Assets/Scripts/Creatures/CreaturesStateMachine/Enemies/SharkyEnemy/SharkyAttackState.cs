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
            Rb2D.velocity = Vector2.zero;
            //Debug.Log("ATTACK ENTERED");
            //Attack();
        }

        public override void Update()
        {
            base.Update();
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)) && StateInfo.normalizedTime > 1.5f)
            {
                StateMachine.ChangeState(Sharky.BattleState);
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