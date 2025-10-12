using Creatures.CreaturesHealth;
using UnityEngine;

namespace Creatures.CreaturesCollisions
{
    public class CombatCollisions : MonoBehaviour
    {
        [Header("Target Detection")] 
        [SerializeField] protected float damage = 10f;
        [SerializeField] protected Transform targetCheck;
        [SerializeField] protected float detectionRadius;
        [SerializeField] protected LayerMask whatIsTarget;
        
        
        public virtual void PerformAttack()
        {
            Collider2D[] colls = GetDetectedColliders();
            
            foreach (var col in colls)
            {
                BasicHealth health = col.gameObject.GetComponent<BasicHealth>();
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