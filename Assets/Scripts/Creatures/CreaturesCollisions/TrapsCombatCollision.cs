using UnityEngine;

namespace Creatures.CreaturesCollisions
{
    public class TrapsCombatCollision : CombatCollisions
    {
        [SerializeField] private Vector2 sizeVision;

        private void Update()
        {
            Collider2D[] colliders = GetDetectedColliders();
            
            Debug.Log(colliders.Length);
        }
        
        public override void PerformAttack()
        {
            base.PerformAttack();
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