using System;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesVFX;
using Creatures.Interfaces;
using UnityEngine;

namespace Creatures.CreaturesHealth
{
    public class CreatureHealth : MonoBehaviour
    {

        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected bool isDead;

        private float _previousHealth;
        private float _currentHealth;
        private CreatureVFX _creaturesVFX;
        private Creature _creature;
        
        public event Action<float, float> OnHealthChange;
        
        protected virtual void Awake()
        {
            _creaturesVFX = GetComponent<CreatureVFX>();
            _creature = GetComponentInParent<Creature>();
            _currentHealth = maxHealth;
            _previousHealth = _currentHealth;
            
            OnHealthChange?.Invoke(_previousHealth, _currentHealth);
        }
        
        public virtual void TakeDamage(float damage, Transform attacker)
        {
            if (isDead) return;

            Vector2 hitDir = CaclulateHitDirection(damage, attacker);
            
            float duration = CalculateDuration(damage);

            // Тут нужно визвать состояние hit
            _creature.SetFinalHitDuration(duration);
            _creature.SetFinalHit(hitDir);
            
            _creature.StateMachine.ChangeState(_creature.HitState);

            ReduceHealth(damage); // важен порядок
            _creaturesVFX.PlayOnDamageVFX();
        }

        private void ReduceHealth(float damage)
        {
            if(isDead) return;
            
            _previousHealth = _currentHealth;
            _currentHealth -= damage;
            
            OnHealthChange?.Invoke(_previousHealth, _currentHealth);

            if (_currentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            _creature.StateMachine.ChangeState(_creature.DeathState);
        }

        private Vector2 CaclulateHitDirection(float damage, Transform attacker)
        {
            int direction = transform.position.x > attacker.position.x ? 1 : -1;

            IFacingDirection facingCreature = attacker.GetComponent<IFacingDirection>();

            if (facingCreature != null)
            {
                int enemyFacingDirectionDirection = attacker.GetComponent<IFacingDirection>().FacingDirection;

                if (enemyFacingDirectionDirection == _creature.FacingDirection)
                {
                    _creature.Flip();
                }
            }


            //Vector2 hitPower = _creature.HitPower;

            Vector2 hitPower = IsHeavyDamage(damage) ? _creature.HeavyHitPower : _creature.HitPower;

            hitPower.x *= direction;
            return hitPower;
        }

        private float CalculateDuration(float damage) =>
            IsHeavyDamage(damage) ? _creature.HeavyHitDuration : _creature.HitDuration;

        private bool IsHeavyDamage(float damage) => 
            damage / maxHealth > _creature.HeavyDamageThreshold;
    }

}