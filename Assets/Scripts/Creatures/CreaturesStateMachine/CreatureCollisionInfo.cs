using Components;
using DefaultNamespace.Utils;
using UnityEngine;

namespace Creatures.CreaturesStateMachine
{
    public class CreatureCollisionInfo : MonoBehaviour
    {
        protected Creature Creature;
        
        [Header("Ground Collision Info")]
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] private Transform groundCheckStartPos;
        [SerializeField] private float groundCheckDistance = 0.1f;
        public bool IsGrounded { get; private set; }
        
        [Header("Wall Collision Info")] 
        [SerializeField] private Transform wallCheckStartPos;
        [SerializeField] private Vector2 wallCheckBoxSize = new Vector2(0.1f, 0.5f);
        [SerializeField] private float wallCheckDistance = 1f;
        public bool IsWallDetected { get; private set; }
        
        [Header("Interaction Collision Info")] 
        [SerializeField] private LayerMask whatIsInteraction;
        [SerializeField] private float interactionRadius;
        private bool _isInteraction;
        private Collider2D[] _interactionCollides = new Collider2D[1];

        [Header("GameObjects Collision Info")] 
        [SerializeField] private float radius = 0.25f;
        [SerializeField] private Vector3 offset = new Vector3(0.65f, 0, 0);
        private readonly Collider2D[] _itemCollider2Ds = new Collider2D[5];
        
        private void Awake()
        {
            Creature = GetComponent<Creature>();
        }
        
        public void HandleWallCheck()
        {
            IsWallDetected = Physics2D.BoxCast(
                wallCheckStartPos.position,
                wallCheckBoxSize,
                0f,
                Vector2.right * Creature.FacingDirection,
                wallCheckDistance,
                whatIsGround
            );
        }

        public void HandleGroundCheck()
        {
            IsGrounded 
                = Physics2D.Raycast(groundCheckStartPos.position, Vector2.down, groundCheckDistance, whatIsGround);
        }
        
        public void Interact()
        {
            var size =
                Physics2D.OverlapCircleNonAlloc
                (transform.position,
                    interactionRadius,
                    _interactionCollides,
                    whatIsInteraction);
            
            for (int i = 0; i < size; i++)
            {
                var interactable = _interactionCollides[i].GetComponent<InteractableComponent>();
                if (interactable != null)
                {
                    interactable.Interact();
                } 
            }
        }

        public GameObject[] GetObjectsInRange()
        {
            var size = 
                Physics2D.OverlapCircleNonAlloc(transform.position + offset * Creature.FacingDirection, radius, _itemCollider2Ds);
            
            var objects = new GameObject[size];
            for (int i = 0; i < size; i++)
            {
                objects[i] = _itemCollider2Ds[i].gameObject;
            }
            
            return objects;
        }

        protected virtual void OnDrawGizmos()
        {
            // GroundCheck
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Vector2 toGround = new Vector2(groundCheckStartPos.position.x, groundCheckStartPos.position.y - groundCheckDistance);
            Gizmos.DrawLine(groundCheckStartPos.position, toGround);
            
            // WallCheck
            Gizmos.color = IsWallDetected ? Color.green : Color.red;
            Gizmos.DrawWireCube(wallCheckStartPos.position, wallCheckBoxSize);
        }
    }
}