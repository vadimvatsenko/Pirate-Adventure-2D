using Creatures.CreaturesHealth;
using UnityEngine;

namespace Creatures.CreaturesCollisions
{
    public class CreatureCombatCollisions : MonoBehaviour
    {
        [Header("Target Detection")] 
        [SerializeField] private float damage = 10f;
        [SerializeField] private Transform targetCheck;
        [SerializeField] private float detectionRadius;
        [SerializeField] private LayerMask whatIsTarget;
        
        public void PerformAttack()
        {
            
            Collider2D[] colls = GetDetectedColliders();
            
            foreach (var col in colls)
            {
                CreatureHealth health = col.gameObject.GetComponent<CreatureHealth>();
                health?.TakeDamage(damage, this.transform);
            }
        }
        private Collider2D[] GetDetectedColliders()
        {
            return Physics2D.OverlapCircleAll(
                    targetCheck.position, 
                    detectionRadius, 
                    whatIsTarget);
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetCheck.position, detectionRadius);
        }
    }
}