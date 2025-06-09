using Components;
using DefaultNamespace.Utils;
using UnityEngine;

namespace Creatures
{
    public class CreatureCollisionInfo : MonoBehaviour
    {
        private Creature _creature;
        
        [Header("Ground Collision Info")] 
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector3 groundCheckOffset;
        [SerializeField] private Vector2 groundBoxSize = new Vector2(0.5f, 0.1f);
        [SerializeField] private float groundCheckDistance;
        private bool _isGrounded;
        
        [Header("Wall Collision Info")] 
        [SerializeField] private Vector3 wallCheckOffset;
        [SerializeField] private Vector2 wallBoxSize = new Vector2(0.1f, 0.5f);
        public bool IsWallDetected { get; private set; }
        
        [Header("Abyss Detected Info")]
        [SerializeField] private Vector3 abyssCheckOffset;
        private bool _isAbyssDetected;
        [Space()]
        [SerializeField] private Vector3 checkGroundAfterAbyssOffset;
        private bool _isGroundAfterAbyss;

        [Header("Interaction Collision Info")] 
        [SerializeField] private LayerMask whatIsInteraction;
        [SerializeField] private float interactionRadius;
        private bool _isInteraction;
        private Collider2D[] _interactionCollides = new Collider2D[1];

        [Header("GameObjects Collision Info")] 
        [SerializeField] private float radius = 0.25f;
        [SerializeField] private Vector3 offset = new Vector3(0.65f, 0, 0);
        private readonly Collider2D[] _itemCollider2Ds = new Collider2D[5];
        
        public bool IsGrounded => _isGrounded;
        public bool IsAbyssDetected => _isAbyssDetected;
        public bool IsGroundAfterAbyss => _isGroundAfterAbyss;

        private void Awake()
        {
            _creature = GetComponent<Creature>();
        }

        public void CheckGroundBeforeCreature()
        {
            Vector2 origin = transform.position;
            // нормализация - это нормализация вектора, приводим его к 1це, при этом сохраняем направление
            Vector2 directionToAbyss = new Vector2(_creature.FacingDirection * abyssCheckOffset.x, abyssCheckOffset.y).normalized;
            // магнитюда - это длинна вектора
            float distanceToAbyss = new Vector2(abyssCheckOffset.x, abyssCheckOffset.y).magnitude;
            
            RaycastHit2D hitAbyss = 
                Physics2D.Raycast(origin, directionToAbyss, distanceToAbyss, whatIsGround);
            _isAbyssDetected = hitAbyss.collider == null; // если не задели землю — значит бездна
            
            Debug.DrawRay(origin, directionToAbyss * distanceToAbyss, 
                _isAbyssDetected ? Color.red : Color.green);
            
            Vector2 directionToGroundAfterAbyss = 
                new Vector2(_creature.FacingDirection * checkGroundAfterAbyssOffset.x, checkGroundAfterAbyssOffset.y).normalized;
            
            float distanceToGroundAfterAbyss = 
                new Vector2(checkGroundAfterAbyssOffset.x, checkGroundAfterAbyssOffset.y).magnitude;
            
            RaycastHit2D hitGroundAfterAbyss = 
                Physics2D.Raycast(origin, directionToGroundAfterAbyss, distanceToGroundAfterAbyss, whatIsGround);
            
            _isGroundAfterAbyss = hitGroundAfterAbyss.collider != null;
            
            Debug.DrawRay
                (origin, directionToGroundAfterAbyss * distanceToGroundAfterAbyss, 
                    _isGroundAfterAbyss? Color.green : Color.red);
        }
        
        public void HandleWallCheck()
        {
            IsWallDetected = 
                Physics2D.BoxCast(
                    transform.position + (wallCheckOffset * _creature.FacingDirection), 
                    wallBoxSize, 
                    20f, 
                    Vector2.right * _creature.FacingDirection, 
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
            var size = Physics2D.OverlapCircleNonAlloc(transform.position + offset * _creature.FacingDirection, radius, _itemCollider2Ds);
            
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
            if (_creature != null)
            {
                Gizmos.color = IsWallDetected ? Color.green : Color.red;
                Gizmos.DrawWireCube(transform.position + (wallCheckOffset * _creature.FacingDirection), wallBoxSize);
            }
            
            // Interaction check circle
            Gizmos.color = _isInteraction ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);

            if (_creature != null)
            {
                Gizmos.color = GetObjectsInRange().Length > 0 ? HandlesUtils.TransparendGreen : HandlesUtils.TransparendRed;
                Gizmos.DrawSphere(transform.position + offset * _creature.FacingDirection, radius);
            }
            
            // пропасть
            /*Gizmos.color = !IsAbyssDetected ? Color.green : Color.red;

            if (_creature != null)
            {
                Vector3 targetPos = 
                    new Vector2(transform.position.x * _creature.FacingDirection + abyssCheckOffset.x, transform.position.y + abyssCheckOffset.y);
                
                Gizmos.DrawLine(transform.position, targetPos);
            }*/
        }

        /*#if UNITY_EDITOR // код вырежется при компиляции, это для того, чтобы прошла компиляция
        private void OnDrawGizmosSelected() // рисует Gizmos когда выделен объект
        {
            if (_player != null)
            {
                Handles.color = HandlesUtils.TransparendRed;
                Handles.DrawSolidDisc(transform.position + offset * _player.FacingDirection, Vector3.forward, radius);
            }
            
        }
        #endif*/
    }
}