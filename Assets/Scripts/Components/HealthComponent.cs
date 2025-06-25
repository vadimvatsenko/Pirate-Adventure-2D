using System;
using Components.HealthComponentFolder;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HealthComponent : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private int health;
        [SerializeField] private UnityEvent onDeath;
        [SerializeField] private UnityEvent onHit;
        
        public void ApplyHeal(int heal)
        {
            throw new System.NotImplementedException();
        }

        public void ApplyDamage(int damage)
        {
            onHit?.Invoke();
            
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                onDeath?.Invoke();
            }
        }

        public void AddHeart()
        {
            throw new System.NotImplementedException();
        }
    }
}