using Components.HealthComponentFolder;
using UnityEngine;

namespace Creatures.CreaturesCollisions
{
    public class TrapsCombatCollision : CombatCollisions
    {
        [SerializeField] private Vector2 sizeVision;

        private void Update()
        {
            Collider2D[] colliders = GetDetectedColliders();
        }
        
        public override void PerformAttack()
        {
            Collider2D[] colls = GetDetectedColliders();
            
            foreach (var col in colls)
            {
                BaseHealthComponent health = col.gameObject.GetComponent<BaseHealthComponent>();
                if (health != null)
                {
                    health?.TakeDamage(damage, this.transform);
                }
            }
        }

        protected override Collider2D[] GetDetectedColliders()
        {
            return Physics2D.OverlapBoxAll(
                targetCheck.position,
                sizeVision,
                whatIsTarget);
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, sizeVision);
        }
    }
}