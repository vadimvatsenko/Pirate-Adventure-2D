using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Enemies.EnemyStates;
using UnityEngine;

namespace Creatures.CreaturesCollisions
{
    public class EnemyCollisionInfo : BasicCollisionInfo
    {
        private Enemy _enemy;
        
        [Header("Hero Detection Collision Info")]
        [SerializeField] private LayerMask whatIsHero;
        [SerializeField] private float distanceToHero;
        [SerializeField] private float attackDistance;
        [SerializeField] private Transform hero;
        public Transform Hero => hero;
        
        [Header("Abyss Detected Info")]
        [SerializeField] private Transform abyssCheckStartPos;
        [SerializeField] private float abyssCheckDistance = 1f;
        public bool IsAbyssDetected { get; private set; }
        
        [Header("Ground After Abyss Detected Info")]
        [SerializeField] private Transform groundAfterAbyssCheckStartPos;
        [SerializeField] private float groundAfterAbyssCheckDistance = 1f;
        
        
        public bool IsGroundAfterAbyssDetected { get; private set; }
        public float DistanceToHero => distanceToHero;
        public float AttackDistance => attackDistance;

        protected override void Awake()
        {
            base.Awake();
            _enemy = GetComponent<Enemy>();
        }

        public RaycastHit2D HeroDetection()
        {
            RaycastHit2D hit = 
                Physics2D.Raycast(Creature.transform.position, 
                                    Vector2.right * Creature.FacingDirection, 
                                    distanceToHero, 
                                    whatIsHero | whatIsGround);

            if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
                return default;
            
            return hit;
        }
        
        public void HandleAbyssCheck()
        {
            IsAbyssDetected = Physics2D.Raycast(
                abyssCheckStartPos.position,
                Vector2.down,
                abyssCheckDistance,
                whatIsGround
            );
            IsAbyssDetected = !IsAbyssDetected;
        }

        public void HandleGroundAfterAbyssCheck()
        {
            IsGroundAfterAbyssDetected = Physics2D.Raycast(
                groundAfterAbyssCheckStartPos.position,
                Vector2.down,
                groundAfterAbyssCheckDistance,
                whatIsGround
            );
        }
        
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            if (Creature != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(Creature.transform.position,
                    new Vector2(Creature.transform.position.x + (Creature.FacingDirection * distanceToHero), 
                        Creature.transform.position.y));
                
                Gizmos.color = Color.green;
                Gizmos.DrawLine(Creature.transform.position,
                    new Vector2(Creature.transform.position.x + (Creature.FacingDirection * _enemy.MinRetreatDistance), 
                        Creature.transform.position.y));
                
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(Creature.transform.position,
                    new Vector2(Creature.transform.position.x + (Creature.FacingDirection * attackDistance), 
                        Creature.transform.position.y));
            }
            
            // Abyss Check
            Gizmos.color = IsAbyssDetected ? Color.green : Color.red;
            Vector2 toAbyss = 
                new Vector2(abyssCheckStartPos.position.x, 
                    abyssCheckStartPos.position.y - abyssCheckDistance);
            Gizmos.DrawLine(abyssCheckStartPos.position, toAbyss);
            
            // Ground After Abyss Check
            Gizmos.color = IsGroundAfterAbyssDetected ? Color.green : Color.red;
            Vector2 toGroundAfterAbyss = 
                new Vector2(groundAfterAbyssCheckStartPos.position.x, 
                    groundAfterAbyssCheckStartPos.position.y - groundAfterAbyssCheckDistance);
            Gizmos.DrawLine(groundAfterAbyssCheckStartPos.position, toGroundAfterAbyss);
        }
    }
}