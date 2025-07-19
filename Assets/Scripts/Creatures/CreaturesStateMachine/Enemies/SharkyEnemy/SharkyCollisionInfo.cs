using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyCollisionInfo : CreatureCollisionInfo
    {
        [Header("Hero Detection Collision Info")]
        [SerializeField] private LayerMask whatIsHero;
        [SerializeField] private float distanceToHero;
        [SerializeField] private Transform hero;
        public bool IsHeroDetected { get; private set; }

        public void CreatureCheck()
        {
            
            IsHeroDetected = Physics2D.Raycast(
                this.transform.position,
                Vector2.right * Creature.FacingDirection,
                distanceToHero, whatIsHero);
            
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            // CreatureDetection
            Gizmos.color = IsHeroDetected ? Color.green : Color.red;
            
            Vector2 toCreature =
                new Vector2(Creature.transform.position.x + (distanceToHero * Creature.FacingDirection) ,
                    Creature.transform.position.y);
            
            Gizmos.DrawLine(Creature.transform.position, toCreature);
        }
    }
}