using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.HealthComponentFolder
{
    public interface IHealthComponent
    {
        UnityEvent OnDeath { get; }
        void TakeHeal(int heal);
        void TakeDamage(int damage);
        void TakeDamage(int damage, Transform damager);
        
    }
}