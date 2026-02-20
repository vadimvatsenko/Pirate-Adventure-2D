using Components.HealthComponentFolder;
using Creatures.CreaturesHealth;
using UnityEngine;

// проверка колизии сражения, при атаке, если кто-то попалает с 
namespace Creatures.CreaturesCollisions
{
    public class CombatCollisions : MonoBehaviour
    {
        [Header("Target Detection")] 
        [SerializeField] protected int damage = 10;
        [SerializeField] protected Transform targetCheck;
        [SerializeField] protected float detectionRadius;
        [SerializeField] protected LayerMask whatIsTarget;
        
        public virtual void PerformAttack()
        {
            Collider2D[] colls = GetDetectedColliders();
            
            foreach (var col in colls)
            {
                IHealthComponent health = col.gameObject.GetComponent<IHealthComponent>();
                if (health != null)
                {
                    health?.TakeDamage(damage, this.transform);
                }
            }
        }
        protected virtual Collider2D[] GetDetectedColliders()
        {
            return Physics2D.OverlapCircleAll(
                    targetCheck.position, 
                    detectionRadius, 
                    whatIsTarget);
        }
        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetCheck.position, detectionRadius);
        }
    }
}