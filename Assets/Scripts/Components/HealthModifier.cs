using UnityEngine;

namespace Components
{
    public class HealthModifier :MonoBehaviour
    {
        [SerializeField] private int healthModifier;
        [SerializeField] private bool isDamager = false;
        public void ApplyHealth(GameObject target)
        {
            Debug.Log(target.name);
            Debug.Log("Health Modify 1");
            var healthComponent = target.GetComponent<HealthComponent>();

            if (healthComponent != null)
            {
                Debug.Log("Health Modify 2");
                if (isDamager)
                    healthComponent.ApplyDamage(healthModifier);
                else 
                    healthComponent.ApplyHeal(healthModifier);
            }
        }
    }
}