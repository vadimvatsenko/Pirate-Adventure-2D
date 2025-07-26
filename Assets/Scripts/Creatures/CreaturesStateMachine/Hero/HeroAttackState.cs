using Components;
using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroAttackState : HeroState
    {
        public HeroAttackState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            
            Attack();
        }

        public override void Update()
        {
            base.Update();
            
            Rb2D.velocity = Vector2.zero;
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Attack)) && StateInfo.normalizedTime > 1.0f)
            {
                StateMachine.ChangeState(Hr.IdleState);
            }
        }
        
        public void Attack()
        {
            
            if (!Hr.GameSess.PlayerData.isArmed || !CollisionInfo.IsGrounded) return;
            
            var gos = CollisionInfo.GetObjectsInRange();
            
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                
                if (hp != null)
                {
                    if(hp.Health <= 0) return;
                    
                    hp.ApplyDamage(Hr.AttackForce);
                    
                    Creature attacker = go.GetComponent<Creature>();

                    if (attacker != null)
                    {
                        if (Hr.FacingDirection == attacker.FacingDirection)
                        {
                            attacker.HandleFlip();
                        }
                    }
                }
            }
        }
    }
}