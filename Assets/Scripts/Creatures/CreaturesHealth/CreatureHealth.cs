using UnityEngine;

namespace Creatures.CreaturesHealth
{
    public class CreatureHealth : MonoBehaviour
    {
        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected bool isDead;

        public virtual void TakeDamage(float damage)
        {
            if(isDead) return;
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