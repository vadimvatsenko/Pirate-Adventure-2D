using System.Linq;
using Components;
using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAttackState : SharkyState
    {
        private bool _damageDealt;
        public SharkyAttackState(SharkyE sharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(sharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Rb2D.velocity = Vector2.zero;
            _damageDealt = false;
        }

        public override void Update()
        {
            base.Update();
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)) && StateInfo.normalizedTime > 1.0f)
            {
                Attack();
                StateMachine.ChangeState(Sharky.IdleState);
            }
        }
        
        public void Attack()
        {
            if(!CollisionInfo.IsGrounded || _damageDealt) return;
            
            var gos = CollisionInfo.GetObjectsInRange();
            
            foreach (var go in gos)
            {
                Debug.Log(go.name);
                var hp = go.GetComponent<IHealthComponent>();
                if (hp != null)
                {
                    hp.ApplyDamage(1);
                    _damageDealt = true;
                    return;
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            _damageDealt = false;
        }
    }
}