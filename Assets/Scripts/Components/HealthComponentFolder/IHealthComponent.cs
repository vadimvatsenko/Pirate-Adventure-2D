using UnityEngine;
using UnityEngine.Events;

namespace Components.HealthComponentFolder
{
    public interface IHealthComponent
    {
        void ApplyHeal(int heal);
        void ApplyDamage(int damage);
        void AddHeart();
    }
}