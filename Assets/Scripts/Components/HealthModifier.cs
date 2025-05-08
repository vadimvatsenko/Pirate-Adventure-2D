using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HealthModifier :MonoBehaviour
    {
        [SerializeField] private int healthModifier;
        [SerializeField] private bool isDamager = false;
        [SerializeField] private UnityEvent onDamaged;
        public void ApplyHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();

            if (healthComponent != null)
            {
                if (isDamager)
                    healthComponent.ApplyDamage(healthModifier);
                else 
                    healthComponent.ApplyHeal(healthModifier);
                
            }
        }
    }
}