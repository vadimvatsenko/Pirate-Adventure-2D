using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.HealthComponentFolder
{
    public class HealthComponent : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private int health;
        [SerializeField] private UnityEvent nDeath;
        [SerializeField] private UnityEvent onHit;
        

        public int Health => health; 
        
        private event Action OnDeath;
        private event Action OnHit;
        public void SubscribeOnHitEvent(Action action) => OnHit += action;
        public void UnsubscribeOnHitEvent(Action action) => OnHit -= action;
        public void SubscribeOnDeathEvent(Action action) => OnDeath += action;
        public void UnsubscribeOnDeathEvent(Action action) => OnDeath -= action;

        

        public void TakeHeal(int heal)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int damage)
        {
            OnHit?.Invoke();
            
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                OnDeath?.Invoke();
            }
        }

        public void TakeDamage(int damage, Transform damager)
        {
            throw new NotImplementedException();
        }

        public void AddHeart()
        {
            throw new System.NotImplementedException();
        }
    }
}