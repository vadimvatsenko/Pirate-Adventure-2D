using System;
using Creatures.CreaturesHealth;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemHealth : BasicHealth
    {
        private TotemTrap _totemTrap;

        
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

                _totemTrap.StateMachine.ChangeState(_totemTrap.AttackState);
            }
        }
    }
}