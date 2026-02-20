using UnityEngine;
using UnityEngine.Events;

namespace Components.HealthComponentFolder
{
    public class ItemHealth : MonoBehaviour, IHealthComponent
    {
        public UnityAction OnDeath { get; private set; }

        public void TakeHeal(int heal)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int damage, Transform damager)
        {
            Debug.Log($"{damage}");
        }
    }
}