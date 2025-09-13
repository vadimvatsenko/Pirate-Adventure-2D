using UnityEngine;

namespace Creatures.CreaturesCollisions
{
    public class HeroCollisionInfo : CreatureCollisionInfo
    {
        [Header("Grabb Collision Info")]
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private Transform grabbingCheckTransform;
        [SerializeField] private float grabbingChecDistance;

        public bool IsGrabb {get; private set;}
        public void CheckHeroGrab()
        {
            IsGrabb = Physics2D.Raycast(
                grabbingCheckTransform.position, 
                Vector2.right * Creature.FacingDirection, 
                grabbingChecDistance, 
                wallLayer);
        }
        
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(grabbingCheckTransform.position, grabbingCheckTransform.right * grabbingChecDistance);
        }
    }
}