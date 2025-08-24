using Creatures.CreaturesVFX;
using UnityEngine;

namespace Creatures.CreaturesHealth
{
    public class CreatureHealth : MonoBehaviour
    {
        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected bool isDead;
        
        private CreatureVFX _creaturesVFX;

        protected virtual void Awake()
        {
            _creaturesVFX = GetComponent<CreatureVFX>();
        }
        public virtual void TakeDamage(float damage, Transform attacker)
        {
            if(isDead) return;
            _creaturesVFX.PlayOnDamageVFX();
            ReduceHealth(damage);
        }

        protected void ReduceHealth(float damage)
        {
            maxHealth -= damage;
            if (maxHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            Debug.Log("Creature Die");
        }
    }
}