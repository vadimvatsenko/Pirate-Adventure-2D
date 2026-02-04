using System.Collections;
using Creatures.CreaturesHealth;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Totems
{
    public class TotemHealth : BasicHealth
    {
        private TotemTrap _totemTrap;

        private Coroutine _coroutine;
        
        private void Start()
        {
            _totemTrap = gameObject.GetComponent<TotemTrap>();
        }

        public override void TakeDamage(float damage, Transform attacker)
        {
            base.TakeDamage(damage, attacker);
            
            if(isDead) return;
            
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