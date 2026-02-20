using Components.HealthComponentFolder;
using Creatures.CreaturesStateMachine.Enemies.EnemyStates;
using UnityEngine;

namespace Creatures.CreaturesHealth
{
    public class EnemyHealth : BasicHealth, IHealthComponent
    {
        private Enemy _enemy;

        private void Start() =>  _enemy = GetComponent<Enemy>();

        public void TakeDamage(int damage, Transform attacker)
        {
            base.TakeDamage(damage, attacker);
            
            if(isDead) return;
            
            if (attacker != null)
            {
                if(_enemy.StateMachine.CurrentState == _enemy.BattleState) return;
                if(_enemy.StateMachine.CurrentState == _enemy.EnemyAttackState) return;
                
                _enemy.StateMachine.ChangeState(_enemy.BattleState);
            }
            
            CreatureVFX.PlayOnDamageVFX();
        }

        public void TakeHeal(int heal)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        
    }
}