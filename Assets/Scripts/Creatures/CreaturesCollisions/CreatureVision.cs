using System.Numerics;
using Creatures.CreaturesHealth;
using UnityEngine;

namespace Creatures.CreaturesCollisions
{
    public class CreatureVision : MonoBehaviour
    {
        [Header("Target Detection")] 
        [SerializeField] private Transform targetCheck;
        [SerializeField] private float detectionDistance;
        [SerializeField] private LayerMask whatIsTarget;

        private int damage;
        private float detectionRadius;
        
        public void PerformAttack()
        {
            Collider2D[] colls = GetDetectedColliders();
            
            foreach (var col in colls)
            {
                CreatureHealth health = col.gameObject.GetComponent<CreatureHealth>();
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
        private void OnDrawGizmos()
        {
            //Gizmos.DrawLine(targetCheck.position, targetCheck.position + );
        }
    }
}