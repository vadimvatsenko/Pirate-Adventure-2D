using Components;
using DefaultNamespace.Utils;
using UnityEngine;

namespace Creatures.CreaturesStateMachine
{
    public class CreatureCollisionInfo : MonoBehaviour
    {
        private Creature _creature;
        
        [Header("Ground Collision Info")]
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Transform groundCheckStartPos;
        [SerializeField] private Vector2 groundCheckBoxSize = new Vector2(0.5f, 0.1f);
        [SerializeField] private float groundCheckDistance = 1f;
        private bool _isGrounded;
        public bool IsGrounded => _isGrounded;
        
        [Header("Wall Collision Info")] 
        [SerializeField] private Transform wallCheckStartPos;
        [SerializeField] private Vector2 wallCheckBoxSize = new Vector2(0.1f, 0.5f);
        [SerializeField] private float wallCheckDistance = 1f;
        public bool _isWallDetected;
        public bool IsWallDetected => _isWallDetected;
        
        [Header("Abyss Detected Info")]
        [SerializeField] private Transform abyssCheckStartPos;
        [SerializeField] private float abyssCheckDistance = 1f;
        private bool _isAbyssDetected;
        public bool IsAbyssDetected => _isAbyssDetected;
        
        [Header("Ground After Abyss Detected Info")]
        [SerializeField] private Transform groundAfterAbyssCheckStartPos;
        [SerializeField] private float groundAfterAbyssCheckDistance = 1f;
        private bool _isGroundAfterAbyssDetected;
        public bool IsGroundAfterAbyssDetected => _isGroundAfterAbyssDetected;
        
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
            _creature = GetComponent<Creature>();
        }
        
        public void HandleWallCheck()
        {
            _isWallDetected = Physics2D.BoxCast(
                wallCheckStartPos.position,
                wallCheckBoxSize,
                0f,
                Vector2.right * _creature.FacingDirection,
                wallCheckDistance,
                whatIsGround
            );
        }

        public void HandleGroundCheck()
        {
            RaycastHit2D groundHit = Physics2D.BoxCast(
                groundCheckStartPos.position,
                groundCheckBoxSize,
                0f,
                Vector2.down,
                0,
                whatIsGround);

            if (groundHit.collider != null)
            {
                float dot = Vector2.Dot(groundHit.normal, Vector2.up);
                _isGrounded = dot >= 0.7f;
            }
            else
            {
                _isGrounded = false;
            }
        }

        public void HandleAbyssCheck()
        {
            _isAbyssDetected = Physics2D.Raycast(
                abyssCheckStartPos.position,
                Vector2.down,
                abyssCheckDistance,
                whatIsGround
            );
            _isAbyssDetected = !_isAbyssDetected;
        }

        public void HandleGroundAfterAbyssCheck()
        {
            _isGroundAfterAbyssDetected = Physics2D.Raycast(
                groundAfterAbyssCheckStartPos.position,
                Vector2.down,
                groundAfterAbyssCheckDistance,
                whatIsGround
            );
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
                Physics2D.OverlapCircleNonAlloc(transform.position + offset * _creature.FacingDirection, radius, _itemCollider2Ds);
            
            var objects = new GameObject[size];
            for (int i = 0; i < size; i++)
            {
                objects[i] = _itemCollider2Ds[i].gameObject;
            }
            
            return objects;
        }

        private void OnDrawGizmos()
        {
            // GroundCheck
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(groundCheckStartPos.position, groundCheckBoxSize);
            
            // WallCheck
            Gizmos.color = _isWallDetected ? Color.green : Color.red;
            Gizmos.DrawWireCube(wallCheckStartPos.position, wallCheckBoxSize);

            // Abyss Check
            Gizmos.color = _isAbyssDetected ? Color.red : Color.green;
            Vector2 toAbyss = 
                new Vector2(abyssCheckStartPos.position.x, 
                    abyssCheckStartPos.position.y - abyssCheckDistance);
            Gizmos.DrawLine(abyssCheckStartPos.position, toAbyss);
            
            // Ground After Abyss Check
            Gizmos.color = _isGroundAfterAbyssDetected ? Color.green : Color.red;
            Vector2 toGroundAfterAbyss = 
                new Vector2(groundAfterAbyssCheckStartPos.position.x, 
                            groundAfterAbyssCheckStartPos.position.y - groundAfterAbyssCheckDistance);
            Gizmos.DrawLine(groundAfterAbyssCheckStartPos.position, toGroundAfterAbyss);
            
            
            
            // Ground check box
            /*Vector3 groundCheckPos = transform.position + groundCheckOffset;
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(groundCheckPos, groundBoxSize);*/

            // Wall check box
            /*if (_creature != null)
            {
                Gizmos.color = IsWallDetected ? Color.green : Color.red;
                Gizmos.DrawWireCube(transform.position + (wallCheckOffset * _creature.FacingDirection), wallBoxSize);
            }*/

            // Interaction check circle
            /*Gizmos.color = _isInteraction ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);*/

            /*if (_creature != null)
            {
                Gizmos.color = GetObjectsInRange().Length > 0
                    ? HandlesUtils.TransparendGreen
                    : HandlesUtils.TransparendRed;
                Gizmos.DrawSphere(transform.position + offset * _creature.FacingDirection, radius);
            }*/
        }
    }
}