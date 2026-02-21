using Creatures.CreaturesStateMachine.Enemies.EnemyStates;
using UnityEngine;
using UnityEngine.Events;

namespace Components.HealthComponentFolder
{
    public class EnemyHealth: BaseHealthComponent
    {
        private Enemy _enemy;
        private void Start() =>  _enemy = GetComponent<Enemy>();

        public override void TakeDamage(int damage, Transform attacker)
        {
            base.TakeDamage(damage, attacker);
            
            if(IsDead) return;
            
            if (attacker != null)
            {
                if(_enemy.StateMachine.CurrentState == _enemy.BattleState) return;
                if(_enemy.StateMachine.CurrentState == _enemy.EnemyAttackState) return;
                
                _enemy.StateMachine.ChangeState(_enemy.BattleState);
            }
        }
    }
}