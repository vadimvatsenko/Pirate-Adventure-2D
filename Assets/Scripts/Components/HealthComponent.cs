using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onDie;

        public void ApplyDamage(int damage)
        {
            health -= damage;
            onDamage?.Invoke();
            
            if (health <= 0)
            {
                onDie?.Invoke();
            }
        }
    }
}