using System;
using Components.HealthComponentFolder;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HealthComponent : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private int health;
        /*[SerializeField] private UnityEvent onDeath;
        [SerializeField] private UnityEvent onHit;*/
        
        public int Health => health; 
        
        private event Action OnDeath;
        private event Action OnHit;
        public void SubscribeOnHitEvent(Action action) => OnHit += action;
        public void UnsubscribeOnHitEvent(Action action) => OnHit -= action;
        public void SubscribeOnDeathEvent(Action action) => OnDeath += action;
        public void UnsubscribeOnDeathEvent(Action action) => OnDeath -= action;
        
        public void ApplyHeal(int heal)
        {
            throw new System.NotImplementedException();
        }

        public void ApplyDamage(int damage)
        {
            OnHit?.Invoke();
            
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                OnDeath?.Invoke();
            }
        }

        public void AddHeart()
        {
            throw new System.NotImplementedException();
        }
    }
}