using UnityEngine;
using UnityEngine.Events;

namespace Components.HealthComponentFolder
{
    public class OtherHealthComponent : MonoBehaviour, IHealthComponent
    {

        [SerializeField] private int health = 3;
        [SerializeField] private int maxHealth = 3;
        
        [SerializeField] private UnityEvent onAddHealth;
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onDie;

        public UnityAction OnHealthChange;
        
        public int Health => health;
        public int MaxHealth => maxHealth;

        public void TakeDamage(int damage)
        {
            health -= damage;

            onDamage?.Invoke();
            OnHealthChange?.Invoke();

            if (health <= 0)
            {
                onDie?.Invoke();
            }
        }

        public void TakeDamage(int damage, Transform damager)
        {
            throw new System.NotImplementedException();
        }

        public void TakeHeal(int heal)
        {
            health += heal;
            if (health > maxHealth)
                health = maxHealth;

            OnHealthChange?.Invoke();
            onAddHealth?.Invoke();
        }

        public void AddHeart()
        {
        }
    }
}