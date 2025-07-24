using Components.HealthComponentFolder;
using Model;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class HeroAttackState : CreatureState
    {
        public HeroAttackState(Hero hero, CreatureStateMachine stateMachine, int animBoolName) 
            : base(hero, stateMachine, animBoolName)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        /*public void Attack()
        {
            if (!Hero.GameSess.PlayerData.isArmed || !CollisionInfo.IsGrounded) return;
            
            var gos = CollisionInfo.GetObjectsInRange();
            
            foreach (var go in gos)
            {
                var hp = go.GetComponent<IHealthComponent>();
                if (hp != null)
                {
                    hp.ApplyDamage(attackForce);
                }
            }
        }*/
    }
}