using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private UnityEvent onAddHealth;
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onDie;

        public void ApplyDamage(int damage)
        {
            health -= damage;
            onDamage?.Invoke();
            
            Debug.Log($"Player Health: {health}");
            
            if (health <= 0)
            {
                Debug.Log($"Player Die");
                onDie?.Invoke();
            }
        }

        public void ApplyHeal(int heal)
        {
            health += heal;
            if (health > maxHealth)
                health = maxHealth;
            
            Debug.Log($"Player Health: {health}");
            
            onAddHealth?.Invoke();
        }
    }
}