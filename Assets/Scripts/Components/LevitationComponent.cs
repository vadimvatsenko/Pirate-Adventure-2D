using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class LevitationComponent : MonoBehaviour
    {
        [Header("Levitation")]
        [SerializeField] private float targetHeight = 2f; // желаемая высота
        [SerializeField] private float levitationForce = 10f; // сила левитации
        [SerializeField] private float damping = 2f; // колебания
        
        [Header("Settings")]
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float rayLength = 5f;
        
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, rayLength, whatIsGround);
            
            if (hit.collider != null)
            {
                float currentHeight = hit.distance;
                float heightError = targetHeight - currentHeight;
                
                float force = heightError * levitationForce - _rb.velocity.y * damping;
                _rb.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * targetHeight);
        }
    }
}