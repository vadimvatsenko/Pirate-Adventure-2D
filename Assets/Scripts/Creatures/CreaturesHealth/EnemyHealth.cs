using Creatures.CreaturesStateMachine.Enemies.EnemyStates;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Creatures.CreaturesHealth
{
    public class EnemyHealth : CreatureHealth
    {
        private Transform _hero;
        private Enemy _enemy;

        private void Start()
        {
            _hero = FindObjectOfType<Hero>().transform;
            _enemy = GetComponent<Enemy>();
        }

        public override void TakeDamage(float damage, Transform attacker)
        {
            base.TakeDamage(damage, attacker);
            
            if(isDead) return;
            
            if (attacker == _hero)
            {
                if(_enemy.StateMachine.CurrentState == _enemy.BattleState) return;
                if(_enemy.StateMachine.CurrentState == _enemy.AttackState) return;
                
                _enemy.StateMachine.ChangeState(_enemy.BattleState);
            }
        }
    }
}