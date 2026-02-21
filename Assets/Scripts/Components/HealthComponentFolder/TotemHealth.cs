using System.Collections;
using Creatures.CreaturesStateMachine.Enemies.Totems;
using UnityEngine;

namespace Components.HealthComponentFolder
{
    public class TotemHealth : BaseHealthComponent
    {
        private TotemTrap _totemTrap;
        private Coroutine _coroutine;
        
        private void Start()
        {
            _totemTrap = gameObject.GetComponent<TotemTrap>();
        }

        public override void TakeDamage(int damage, Transform attacker)
        {
            base.TakeDamage(damage, attacker);
            
            if (attacker != null)
            {
                if(_totemTrap.StateMachine.CurrentState == _totemTrap.AttackState) return;
                _coroutine = StartCoroutine(AttackStateDelay());
            }
        }

        private IEnumerator AttackStateDelay()
        {
            yield return new WaitForSeconds(1f);
            {
                _totemTrap.StateMachine.ChangeState(_totemTrap.AttackState);
            }
        }
    }
}