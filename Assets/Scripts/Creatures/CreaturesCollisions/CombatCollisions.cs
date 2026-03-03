using Components.HealthComponentFolder;
using UnityEngine;

// проверка колизии сражения, при атаке, если кто-то попадает с 
namespace Creatures.CreaturesCollisions
{
    public class CombatCollisions : MonoBehaviour
    {
        [Header("Target Detection")] 
        [SerializeField] protected int damage = 10;
        [SerializeField] protected Transform targetCheck;
        [SerializeField] protected float detectionRadius;
        [SerializeField] protected LayerMask whatIsTarget;
        
        // вчасности это событие, при столкновении или а аниматоре
        public virtual void PerformAttack()
        {
            Collider2D[] colls = GetDetectedColliders();
            
            foreach (var col in colls)
            {
                BaseHealthComponent health = col.gameObject.GetComponent<BaseHealthComponent>();
                
                if (health != null)
                {
                    if(health.IsDead) return;
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