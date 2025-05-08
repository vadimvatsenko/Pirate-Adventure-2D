using Components;
using DefaultNamespace.Utils;
using UnityEditor;
using UnityEngine;

namespace PlayerFolder
{
    public class PlayerCollisionInfo : MonoBehaviour
    {
        private Player _player;
        
        [Header("Ground Collision Info")] 
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector3 groundCheckOffset;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
        private bool _isGrounded;
        
        [Header("Wall Collision Info")] 
        [SerializeField] private Vector3 wallCheckOffset;
        [SerializeField] private Vector2 wallBoxSize = new Vector2(0.1f, 0.5f);
        public bool IsWallDetected { get; private set; }

        [Header("Interaction Collision Info")] 
        [SerializeField] private LayerMask whatIsInteraction;
        [SerializeField] private float interactionRadius;
        private bool _isInteraction;
        private Collider2D[] _interactionCollides = new Collider2D[1];

        [Header("GameObjects Collision Info")] 
        [SerializeField] private float radius = 1f;

        [SerializeField] private Vector3 offset;
        private readonly Collider2D[] _itemCollider2Ds = new Collider2D[5];
        
        public bool IsGrounded => _isGrounded;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }
        
        public void HandleWallCheck()
        {
            IsWallDetected = 
                Physics2D.BoxCast(
                    transform.position + (wallCheckOffset * _player.FacingDirection), 
                    wallBoxSize, 
                    0, 
                    Vector2.right * _player.FacingDirection, 
                    0, 
                    whatIsGround);
        }

        public void HandleGroundCheck()
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                transform.position + groundCheckOffset, 
                groundBoxSize, 
                0f, 
                Vector2.down, 
                groundCheckDistance,
                whatIsGround);

            if (hit.collider != null)
            {
                float dot = Vector2.Dot(hit.normal, Vector2.up);
                _isGrounded = dot >= 0.7f;
            }
            else
            {
                _isGrounded = false;
            }
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
            var size = Physics2D.OverlapCircleNonAlloc(transform.position + offset * _player.FacingDirection, radius, _itemCollider2Ds);
            
            var objects = new GameObject[size];
            for (int i = 0; i < size; i++)
            {
                objects[i] = _itemCollider2Ds[i].gameObject;
            }
            
            return objects;
        }
        
        private void OnDrawGizmos()
        {
            // Ground check box
            Vector3 groundCheckPos = transform.position + groundCheckOffset;
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(groundCheckPos, groundBoxSize);

            // Wall check box
            if (_player != null)
            {
                Gizmos.color = IsWallDetected ? Color.green : Color.red;
                Gizmos.DrawWireCube(transform.position + (wallCheckOffset * _player.FacingDirection), wallBoxSize);
            }
            
            // Interaction check circle
            Gizmos.color = _isInteraction ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }

        #if UNITY_EDITOR // код вырежется при компиляции, это для того, чтобы прошла компиляция
        private void OnDrawGizmosSelected()
        {
            if (_player != null)
            {
                Handles.color = HandlesUtils.TransparendRed;
                Handles.DrawSolidDisc(transform.position + offset * _player.FacingDirection, Vector3.forward, radius);
            }
            
        }
        #endif
    }
}