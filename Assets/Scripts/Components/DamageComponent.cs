using UnityEngine;

namespace Components
{
    public class DamageComponent :MonoBehaviour
    {
        [SerializeField] private int damage;
        public void ApplyDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            healthComponent?.ApplyDamage(damage);
        }
    }
}