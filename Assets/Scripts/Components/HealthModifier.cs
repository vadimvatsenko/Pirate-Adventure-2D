using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    enum HealthModifierType
    {
        Health,
        Damage,
        AddHeart
    }
    public class HealthModifier :MonoBehaviour
    {
        [SerializeField] private int healthModifier;
        [SerializeField] private HealthModifierType whatIsHealth;
        [SerializeField] private UnityEvent onDamaged;
        public void ApplyHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();

            if (healthComponent != null)
            {
                switch (whatIsHealth)
                {
                    case HealthModifierType.Health:
                        healthComponent.ApplyHeal(healthModifier);
                        break;
                    case HealthModifierType.Damage:
                        healthComponent.ApplyDamage(healthModifier);
                        break;
                    case HealthModifierType.AddHeart:
                        healthComponent.AddHeart();
                        break;
                }
            }
        }
    }
}