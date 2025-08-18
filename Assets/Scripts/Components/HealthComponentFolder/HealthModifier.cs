using UnityEngine;
using UnityEngine.Events;

namespace Components.HealthComponentFolder
{
    public class HealthModifier :MonoBehaviour
    {
        [SerializeField] private int healthModifier;
        [SerializeField] private HealthModifierType whatIsHealth;
        
        [Header("Damager Settings")]
        [SerializeField] private bool isModifierDedly = false;
        [SerializeField] private UnityEvent onDamaged;
        public HealthModifierType WhatIsHealth => whatIsHealth;
        public void ApplyHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<IHealthComponent>();

            if (healthComponent != null)
            {
                switch (whatIsHealth)
                {
                    case HealthModifierType.Health:
                        healthComponent.ApplyHeal(healthModifier);
                        break;
                    case HealthModifierType.Damage:
                        if (isModifierDedly)
                        {
                            healthComponent.ApplyDamage(100);
                        }
                        else
                        {
                            healthComponent.ApplyDamage(healthModifier);
                        }
                        break;
                    case HealthModifierType.AddHeart:
                        healthComponent.AddHeart();
                        break;
                }
            }
        }
    }
}