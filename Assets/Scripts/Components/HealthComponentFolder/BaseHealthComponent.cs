using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Components.HealthComponentFolder
{
    public class BaseHealthComponent : MonoBehaviour
    {
        [SerializeField] protected int health; 
        [SerializeField] protected UnityEvent onDeath;
        [SerializeField] protected UnityEvent onTakeDamage;
        public bool IsDead { get; protected set;}
        public int Health { get; protected set; }
        
        public virtual void TakeHeal(int heal)
        {
            throw new System.NotImplementedException();
        }
        
        public virtual void TakeDamage(int damage, Transform damager)
        {
            if(IsDead) return;
            onTakeDamage?.Invoke();
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                onDeath?.Invoke();
                IsDead = true;
                Collider2D collider2D = GetComponent<Collider2D>();
                collider2D.enabled = false;
            }
        }
    }
}